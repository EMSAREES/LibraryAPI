using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Loans;

/// <summary>
/// Se lanza cuando se intenta cancelar un préstamo que ya fue devuelto,
/// ya estaba cancelado, o se encuentra en un estado que no permite cancelación.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class LoanCannotBeCancelledException : DomainException
{
    public LoanCannotBeCancelledException()
        : base("LOAN_CANNOT_BE_CANCELLED", DomainErrors.Loan.CannotCancel) { }
}