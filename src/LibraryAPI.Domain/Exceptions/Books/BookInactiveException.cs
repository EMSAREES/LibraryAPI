using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Books;

/// <summary>
/// Se lanza cuando se intenta operar (prestar, reservar) un libro
/// que fue desactivado del catálogo por un administrador.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class BookInactiveException : DomainException
{
    public BookInactiveException()
        : base("BOOK_INACTIVE", DomainErrors.Book.IsInactive) { }
}