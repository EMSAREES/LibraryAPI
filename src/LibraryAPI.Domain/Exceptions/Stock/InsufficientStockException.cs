using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Stock;

/// <summary>
/// Se lanza cuando se intenta prestar un libro pero las copias disponibles
/// en la sucursal son cero.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class InsufficientStockException : DomainException
{
    public int AvailableCopies { get; }

    public InsufficientStockException(int availableCopies)
        : base("INSUFFICIENT_STOCK", DomainErrors.Stock.InsufficientCopies)
    {
        AvailableCopies = availableCopies;
    }
}