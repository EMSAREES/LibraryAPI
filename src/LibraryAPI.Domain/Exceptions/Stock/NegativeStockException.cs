using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Stock;

/// <summary>
/// Se lanza cuando una operación de devolución o baja de inventario
/// resultaría en un número negativo de copias disponibles.
/// HTTP → 422 Unprocessable Entity.
/// </summary>
public sealed class NegativeStockException : DomainException
{
    public NegativeStockException()
        : base("NEGATIVE_STOCK", DomainErrors.Stock.NegativeStock) { }
}