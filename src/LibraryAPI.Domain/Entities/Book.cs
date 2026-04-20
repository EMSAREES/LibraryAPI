namespace LibraryAPI.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }

    public string Title { get; private set; } = default!;
    public string ISBN { get; private set; } = default!;
    public string? Synopsis { get; private set; }

    public int PublicationYear { get; private set; }
    public string Language { get; private set; } = default!;
    public int? TotalPages { get; private set; }

    public string? CoverImageUrl { get; private set; }
    public string? BackCoverImageUrl { get; private set; }

    public bool IsActive { get; private set; } = true;

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Guid CreatedByUserId { get; private set; }
}