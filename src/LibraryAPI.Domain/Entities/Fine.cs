namespace LibraryAPI.Domain.Entities;

public class Fine
{
    public Guid Id { get; private set; }

    public Guid LoanId { get; private set; }
    public Guid UserId { get; private set; }

    public int OverdueDays { get; private set; }

    public decimal AmountPerDay { get; private set; }
    public decimal TotalAmount { get; private set; }

    public bool IsPaid { get; private set; } = false;
    public DateTime? PaidAt { get; private set; }

    public Guid? PaidByUserId { get; private set; }

    public DateTime CreatedAt { get; private set; }
}