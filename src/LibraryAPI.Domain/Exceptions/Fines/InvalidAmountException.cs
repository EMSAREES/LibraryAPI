using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Fines;

/// <summary>
/// Se lanza cuando se intenta registrar el pago de una multa con un monto inválido (ej. negativo o cero).
/// HTTP → 400 Bad Request.
/// El monto total de multas pendientes del usuario se incluye en la excepción para proporcionar contexto adicional.
/// </summary>
public sealed class InvalidAmountException : DomainException
{
    /// <summary>
    /// Monto total de multas pendientes del usuario.
    /// </summary>
    public decimal InvalidAmount { get; }

    public InvalidAmountException(decimal invalidAmount)
        : base("INVALID_AMOUNT", DomainErrors.Fine.InvalidAmount)
    {
        InvalidAmount = invalidAmount;
    }
}