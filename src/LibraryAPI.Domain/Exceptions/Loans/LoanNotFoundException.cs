using LibraryAPI.Domain.Exceptions.Base;

namespace LibraryAPI.Domain.Exceptions.Loans;

/// <summary>
/// Se lanza cuando se busca un préstamo por ID y no existe en el sistema.
/// HTTP → 404 Not Found.
/// </summary>
public sealed class LoanNotFoundException : DomainException
{
    public Guid LoanId { get; }

    public LoanNotFoundException(Guid loanId)
        : base("LOAN_NOT_FOUND", "El préstamo solicitado no fue encontrado.")
    {
        LoanId = loanId;
    }
}