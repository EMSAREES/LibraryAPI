using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Stock;

/// <summary>
/// Se lanza cuando se intenta incrementar las copias disponibles por encima
/// del total de copias físicas registradas para esa sucursal.
/// HTTP → 422 Unprocessable Entity.
/// </summary>
public sealed class StockExceedsTotalCopiesException : DomainException
{
    public StockExceedsTotalCopiesException()
        : base("STOCK_EXCEEDS_TOTAL", DomainErrors.Stock.ExceedsTotalCopies) { }
}