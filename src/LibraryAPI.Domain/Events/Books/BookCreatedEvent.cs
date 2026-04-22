using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Books;

/// <summary>
/// Se publica cuando un nuevo libro es registrado en el catálogo.
/// </summary>
public sealed class BookCreatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Libro que fue registrado en el catálogo.
    /// </summary>
    public Book Book { get; }

    public BookCreatedEvent(Book book)
    {
        Book = book;
    }
}