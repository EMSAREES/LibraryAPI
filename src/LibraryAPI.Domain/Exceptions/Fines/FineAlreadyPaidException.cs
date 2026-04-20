using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Fines;

/// <summary>
/// Se lanza cuando se intenta registrar el pago de una multa
/// que ya fue pagada anteriormente.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class FineAlreadyPaidException : DomainException
{
    public FineAlreadyPaidException()
        : base("FINE_ALREADY_PAID", DomainErrors.Fine.AlreadyPaid) { }
}