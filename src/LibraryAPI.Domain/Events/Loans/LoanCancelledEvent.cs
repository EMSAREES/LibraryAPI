using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Loans;

/// <summary>
/// Se publica cuando un préstamo es cancelado antes de completarse.
/// Los handlers deben incrementar el stock disponible del libro.
/// </summary>
public sealed class LoanCancelledEvent : BaseDomainEvent
{
    /// <summary>
    /// Préstamo que fue cancelado.
    /// </summary>
    public Loan Loan { get; }

    public LoanCancelledEvent(Loan loan)
    {
        Loan = loan;
    }
}