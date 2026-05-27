using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;
 
namespace LibraryAPI.Domain.Exceptions.Books;
 
/// <summary>
/// Se lanza cuando se intenta registrar un libro con un ISBN
/// que ya existe en el catálogo del sistema.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class BookAlreadyExistsException : DomainException
{
    public string Isbn { get; }
 
    // CORREGIDO: Usar DomainErrors en lugar de mensaje hardcodeado en español.
    public BookAlreadyExistsException(string isbn)
        : base("BOOK_ALREADY_EXISTS", DomainErrors.Book.BookNotFound)
    {
        Isbn = isbn;
    }
}
 