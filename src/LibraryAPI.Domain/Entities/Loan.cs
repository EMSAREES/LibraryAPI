namespace LibraryAPI.Domain.Entities;

public class Loan
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public Guid BranchId { get; private set; }

    public Guid LoanedByUserId { get; private set; }

    public DateTime LoanDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }

    public string Status { get; private set; } = default!;
    public string? Notes { get; private set; }

    public DateTime CreatedAt { get; private set; }
}