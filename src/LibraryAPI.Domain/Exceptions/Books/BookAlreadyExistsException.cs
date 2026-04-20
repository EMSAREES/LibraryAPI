using LibraryAPI.Domain.Exceptions.Base;

namespace LibraryAPI.Domain.Exceptions.Books;

/// <summary>
/// Se lanza cuando se intenta registrar un libro con un ISBN
/// que ya existe en el catálogo del sistema.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class BookAlreadyExistsException : DomainException
{
    public string Isbn { get; }

    public BookAlreadyExistsException(string isbn)
        : base("BOOK_ALREADY_EXISTS", $"Ya existe un libro registrado con el ISBN {isbn}.")
    {
        Isbn = isbn;
    }
}