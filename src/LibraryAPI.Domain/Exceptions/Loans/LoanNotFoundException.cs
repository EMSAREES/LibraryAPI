using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Loans;

/// <summary>
/// Se lanza cuando se busca un préstamo por ID y no existe en el sistema.
/// HTTP → 404 Not Found.
/// </summary>
public sealed class LoanNotFoundException : DomainException
{
    public Guid LoanId { get; }

    public LoanNotFoundException(Guid loanId)
        : base("LOAN_NOT_FOUND", DomainErrors.Loan.LoanNotFound)
    {
        LoanId = loanId;
    }
}