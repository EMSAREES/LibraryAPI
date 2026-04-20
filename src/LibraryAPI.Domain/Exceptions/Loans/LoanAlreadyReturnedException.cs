using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Loans;

/// <summary>
/// Se lanza cuando se intenta devolver un libro cuyo préstamo
/// ya fue cerrado como devuelto anteriormente.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class LoanAlreadyReturnedException : DomainException
{
    public LoanAlreadyReturnedException()
        : base("LOAN_ALREADY_RETURNED", DomainErrors.Loan.AlreadyReturned) { }
}