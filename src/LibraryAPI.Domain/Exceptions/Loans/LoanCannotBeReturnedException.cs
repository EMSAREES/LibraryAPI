using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Loans;

/// <summary>
/// Se lanza cuando se intenta registrar la devolución de un préstamo
/// que no está en estado Active u Overdue.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class LoanCannotBeReturnedException : DomainException
{
    public LoanCannotBeReturnedException()
        : base("LOAN_CANNOT_BE_RETURNED", DomainErrors.Loan.CannotReturn) { }
}