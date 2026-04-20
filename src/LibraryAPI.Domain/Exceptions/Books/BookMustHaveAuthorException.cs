using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Books;

/// <summary>
/// Se lanza cuando se intenta guardar un libro sin ningún autor asociado.
/// Un libro siempre debe tener al menos un autor registrado.
/// HTTP → 422 Unprocessable Entity.
/// </summary>
public sealed class BookMustHaveAuthorException : DomainException
{
    public BookMustHaveAuthorException()
        : base("BOOK_MUST_HAVE_AUTHOR", DomainErrors.Book.MustHaveAuthor) { }
}