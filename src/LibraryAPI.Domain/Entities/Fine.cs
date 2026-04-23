using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Events.Fines;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Exceptions.Fines;

namespace LibraryAPI.Domain.Entities;

public sealed class Fine : BaseEntity
{
    public Guid LoanId { get; private set; }
    public Guid UserId { get; private set; }
    public int OverdueDays { get; private set; }
    public decimal AmountPerDay { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsPaid { get; private set; }
    public DateTime? PaidAt { get; private set; }
    public Guid? PaidByUserId { get; private set; }

    private Fine() { }

    private Fine(Guid loanId, Guid userId, int overdueDays, decimal amountPerDay, Guid createdByUserId)
    {
        LoanId = loanId;
        UserId = userId;
        OverdueDays = overdueDays;
        AmountPerDay = amountPerDay;
        TotalAmount = overdueDays * amountPerDay;
        IsPaid = false;
        CreatedByUserId = createdByUserId;
    }

    public static Fine Create(Guid loanId, Guid userId, int overdueDays, decimal amountPerDay, Guid createdByUserId)
    {
        if (loanId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(loanId)
            };

        if (userId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(userId)
            };

        if (createdByUserId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(createdByUserId)
            };

        if (overdueDays <= 0)
            throw new DomainValidationException(DomainErrors.General.InvalidValue)
            {
                FieldName = nameof(overdueDays)
            };

        if (amountPerDay <= 0)
            throw new InvalidAmountException(amountPerDay);

        var fine = new Fine(loanId, userId, overdueDays, amountPerDay, createdByUserId);
        fine.AddDomainEvent(new FineCreatedEvent(fine));
        return fine;
    }

    public void Pay(Guid paidByUserId, DateTime? paidAt = null)
    {
        if (IsPaid)
            throw new FineAlreadyPaidException();

        if (paidByUserId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(paidByUserId)
            };

        IsPaid = true;
        PaidByUserId = paidByUserId;
        PaidAt = paidAt ?? DateTime.UtcNow;

        MarkAsUpdated();
        AddDomainEvent(new FinePaidEvent(this, paidByUserId));
    }
}