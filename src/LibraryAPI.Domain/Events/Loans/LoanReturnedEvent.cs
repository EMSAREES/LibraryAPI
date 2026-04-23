using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Loans;

/// <summary>
/// Se publica cuando un libro es devuelto correctamente.
/// Los handlers deben verificar si hay reservas pendientes
/// para ese libro y notificar al siguiente usuario en cola.
/// </summary>
public sealed class LoanReturnedEvent : BaseDomainEvent
{
    /// <summary>
    /// Préstamo que fue cerrado como devuelto.
    /// </summary>
    public Loan Loan { get; }

    /// <summary>
    /// Fecha y hora exacta en que se registró la devolución.
    /// </summary>
    public DateTime ReturnedAt { get; }

    public LoanReturnedEvent(Loan loan, DateTime returnedAt)
    {
        Loan = loan;
        ReturnedAt = returnedAt;
    }
}