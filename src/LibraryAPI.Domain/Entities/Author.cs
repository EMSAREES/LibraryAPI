namespace LibraryAPI.Domain.Entities;

public class Author
{
    public Guid Id { get; private set; }

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Nationality { get; private set; } = default!;
    public DateTime? BirthDate { get; private set; }
    public string? Biography { get; private set; }

    public DateTime CreatedAt { get; private set; }
}