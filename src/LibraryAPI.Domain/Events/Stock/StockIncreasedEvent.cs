using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Stock;

/// <summary>
/// Se publica cuando se agregan copias físicas de un libro a una sucursal.
/// </summary>
public sealed class StockIncreasedEvent : BaseDomainEvent
{
    /// <summary>
    /// Stock del libro que fue incrementado.
    /// </summary>
    public BookStock BookStock { get; }

    /// <summary>
    /// Cantidad de copias que fueron agregadas.
    /// </summary>
    public int CopiesAdded { get; }

    public StockIncreasedEvent(BookStock bookStock, int copiesAdded)
    {
        BookStock = bookStock;
        CopiesAdded = copiesAdded;
    }
}