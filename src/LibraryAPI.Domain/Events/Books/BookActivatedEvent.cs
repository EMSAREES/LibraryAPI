using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Books;

/// <summary>
/// Se publica cuando un libro es reactivado en el catálogo.
/// Los handlers pueden reaccionar notificando a usuarios con reservas
/// pendientes de ese libro o actualizando índices de búsqueda.
/// </summary>
public sealed class BookActivatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Libro que fue reactivado.
    /// </summary>
    public Book Book { get; }

    public BookActivatedEvent(Book book)
    {
        Book = book;
    }
}