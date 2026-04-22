using LibraryAPI.Domain.Exceptions.Base;

namespace LibraryAPI.Domain.Exceptions.Books;

/// <summary>
/// Se lanza cuando se intenta registrar un libro sin proporcionar un título.
/// HTTP → 400 Bad Request.
/// </summary>
public sealed class BookTitleRequiredException : DomainException
{
    public string Title { get; }

    public BookTitleRequiredException(string title)
        : base("BOOK_TITLE_REQUIRED", $"El título del libro es requerido.")
    {
        Title = title;
    }
}