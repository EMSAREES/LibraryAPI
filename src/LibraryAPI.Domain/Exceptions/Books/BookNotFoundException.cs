using LibraryAPI.Domain.Exceptions.Base;

namespace LibraryAPI.Domain.Exceptions.Books;

/// <summary>
/// Se lanza cuando se busca un libro por ID o ISBN y no existe en el catálogo.
/// HTTP → 404 Not Found.
/// </summary>
public sealed class BookNotFoundException : DomainException
{
    public Guid BookId { get; }

    public BookNotFoundException(Guid bookId)
        : base("BOOK_NOT_FOUND", "El libro solicitado no fue encontrado en el catálogo.")
    {
        BookId = bookId;
    }
}