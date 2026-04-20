namespace LibraryAPI.Domain.Entities;

public class BookStock
{
    public Guid Id { get; private set; }

    public Guid BookId { get; private set; }
    public Guid BranchId { get; private set; }

    public int TotalCopies { get; private set; }
    public int AvailableCopies { get; private set; }

    public DateTime UpdatedAt { get; private set; }
}