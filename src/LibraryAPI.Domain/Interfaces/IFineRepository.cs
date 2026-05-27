using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Repositorio de multas. Maneja consultas sobre el estado
/// de deudas y pagos de los usuarios.
/// </summary>
public interface IFineRepository : IRepository<Fine>
{
    /// <summary>
    /// Obtiene todas las multas sin pagar de un usuario.
    /// Se usa para verificar si puede realizar préstamos o reservas.
    /// </summary>
    Task<IReadOnlyList<Fine>> GetUnpaidByUserAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Suma el total de multas pendientes de un usuario.
    /// Más eficiente que cargar la lista completa para sumar.
    /// </summary>
    Task<decimal> GetTotalUnpaidByUserAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si un usuario tiene alguna multa sin pagar.
    /// Se usa antes de crear un préstamo o reserva.
    /// </summary>
    Task<bool> HasUnpaidFinesAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene la multa asociada a un préstamo específico.
    /// Retorna null si el préstamo no generó multa o fue pagada.
    /// </summary>
    Task<Fine?> GetByLoanIdAsync(Guid loanId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el historial completo de multas de un usuario (pagadas y pendientes).
    /// </summary>
    Task<IReadOnlyList<Fine>> GetHistoryByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}