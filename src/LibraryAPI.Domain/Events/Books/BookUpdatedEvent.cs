using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Books;

/// <summary>
/// Se publica cuando los datos de un libro son actualizados.
/// </summary>
public sealed class BookUpdatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Libro con los datos actualizados.
    /// </summary>
    public Book Book { get; }

    public BookUpdatedEvent(Book book)
    {
        Book = book;
    }
}