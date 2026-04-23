using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Enums;
using LibraryAPI.Domain.Events.Reservations;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Exceptions.Reservations;

namespace LibraryAPI.Domain.Entities;

public sealed class Reservation : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public Guid BranchId { get; private set; }
    public ReservationStatus Status { get; private set; }
    public DateTime ReservationDate { get; private set; }
    public DateTime? NotifiedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }

    private Reservation() { }

    private Reservation(Guid userId, Guid bookId, Guid branchId, DateTime reservationDate)
    {
        UserId = userId;
        BookId = bookId;
        BranchId = branchId;
        ReservationDate = reservationDate;
        Status = ReservationStatus.Pending;

        CreatedByUserId = userId;
    }

    public static Reservation Create(Guid userId, Guid bookId, Guid branchId, DateTime reservationDate)
    {
        if (userId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(userId)
            };

        if (bookId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(bookId)
            };

        if (branchId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(branchId)
            };

        var reservation = new Reservation(userId, bookId, branchId, reservationDate);
        reservation.AddDomainEvent(new ReservationCreatedEvent(reservation));
        return reservation;
    }

    public void Notify(DateTime notifiedAt, DateTime expiresAt)
    {
        if (Status == ReservationStatus.Expired)
            throw new ReservationAlreadyExpiredException();

        if (Status != ReservationStatus.Pending)
            throw new DomainValidationException(DomainErrors.General.InvalidValue)
            {
                FieldName = nameof(Status)
            };

        if (expiresAt < notifiedAt)
            throw new DomainValidationException(DomainErrors.General.InvalidDateRange)
            {
                FieldName = nameof(expiresAt)
            };

        Status = ReservationStatus.Notified;
        NotifiedAt = notifiedAt;
        ExpiresAt = expiresAt;

        MarkAsUpdated();
        AddDomainEvent(new ReservationNotifiedEvent(this));
    }

    public void Fulfill()
    {
        if (Status == ReservationStatus.Expired)
            throw new ReservationAlreadyExpiredException();

        if (Status != ReservationStatus.Notified)
            throw new ReservationCannotBeFulfilledException();

        Status = ReservationStatus.Fulfilled;

        MarkAsUpdated();
        AddDomainEvent(new ReservationFulfilledEvent(this));
    }

    public void Cancel()
    {
        if (Status == ReservationStatus.Expired)
            throw new ReservationAlreadyExpiredException();

        if (Status is ReservationStatus.Cancelled or ReservationStatus.Fulfilled)
            throw new ReservationCannotBeCancelledException();

        Status = ReservationStatus.Cancelled;

        MarkAsUpdated();
        AddDomainEvent(new ReservationCancelledEvent(this));
    }

    public void Expire()
    {
        if (Status is ReservationStatus.Expired or ReservationStatus.Cancelled or ReservationStatus.Fulfilled)
            return;

        Status = ReservationStatus.Expired;

        MarkAsUpdated();
        AddDomainEvent(new ReservationExpiredEvent(this));
    }
}