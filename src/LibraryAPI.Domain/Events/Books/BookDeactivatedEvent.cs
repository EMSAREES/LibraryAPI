using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Books;

/// <summary>
/// Se publica cuando un libro es desactivado del catálogo.
/// Los handlers pueden reaccionar cancelando reservas activas
/// sobre ese libro.
/// </summary>
public sealed class BookDeactivatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Libro que fue desactivado.
    /// </summary>
    public Book Book { get; }

    public BookDeactivatedEvent(Book book)
    {
        Book = book;
    }
}