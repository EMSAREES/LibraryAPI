using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Loans;

/// <summary>
/// Se publica cuando un préstamo es creado exitosamente.
/// Los handlers pueden reaccionar enviando una confirmación al usuario
/// con la fecha límite de devolución.
/// </summary>
public sealed class LoanCreatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Préstamo que fue creado.
    /// </summary>
    public Loan Loan { get; }

    public LoanCreatedEvent(Loan loan)
    {
        Loan = loan;
    }
}