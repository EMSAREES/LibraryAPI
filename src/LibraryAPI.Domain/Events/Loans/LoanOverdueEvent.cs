using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Loans;

/// <summary>
/// Se publica cuando un préstamo supera su fecha límite de devolución.
/// Los handlers deben generar la multa correspondiente
/// y bloquear al usuario si aplica.
/// </summary>
public sealed class LoanOverdueEvent : BaseDomainEvent
{
    /// <summary>
    /// Préstamo que superó su fecha límite.
    /// </summary>
    public Loan Loan { get; }

    /// <summary>
    /// Cantidad de días de retraso al momento de detectar el vencimiento.
    /// </summary>
    public int OverdueDays { get; }

    public LoanOverdueEvent(Loan loan, int overdueDays)
    {
        Loan = loan;
        OverdueDays = overdueDays;
    }
}