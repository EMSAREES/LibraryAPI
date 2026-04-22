using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Stock;

/// <summary>
/// Se publica cuando se reducen copias de un libro en una sucursal.
/// Puede ocurrir al crear un préstamo o cuando el admin
/// da de baja ejemplares dañados o perdidos.
/// Los handlers pueden verificar si quedan reservas pendientes
/// que ya no podrán cumplirse.
/// </summary>
public sealed class StockDecreasedEvent : BaseDomainEvent
{
    /// <summary>
    /// Stock del libro que fue decrementado.
    /// </summary>
    public BookStock BookStock { get; }

    /// <summary>
    /// Cantidad de copias que fueron retiradas en esta operación.
    /// </summary>
    public int CopiesRemoved { get; }

    /// <summary>
    /// Copias disponibles después del decremento.
    /// </summary>
    public int AvailableAfter { get; }

    /// <summary>
    /// Indica si el libro quedó sin copias disponibles tras la operación.
    /// Útil para que los handlers decidan si notificar algo.
    /// </summary>
    public bool IsNowUnavailable { get; }

    public StockDecreasedEvent(BookStock bookStock, int copiesRemoved)
    {
        BookStock = bookStock;
        CopiesRemoved = copiesRemoved;
        AvailableAfter = bookStock.AvailableCopies;
        IsNowUnavailable = bookStock.AvailableCopies == 0;
    }
}