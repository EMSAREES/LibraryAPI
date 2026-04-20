using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Reservations;

/// <summary>
/// Se lanza cuando el usuario intenta reservar un libro para el que
/// ya tiene una reserva activa en estado Pending o Notified.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class ReservationAlreadyPendingException : DomainException
{
    public ReservationAlreadyPendingException()
        : base("RESERVATION_ALREADY_PENDING", DomainErrors.Reservation.AlreadyPending) { }
}