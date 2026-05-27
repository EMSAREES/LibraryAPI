using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Enums;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Repositorio de préstamos. Centraliza todas las consultas
/// del ciclo de vida de un préstamo (activo, vencido, devuelto, cancelado).
/// </summary>
public interface ILoanRepository : IRepository<Loan>
{
    /// <summary>
    /// Obtiene todos los préstamos activos de un usuario.
    /// Se usa para validar el límite máximo de préstamos simultáneos.
    /// </summary>
    Task<IReadOnlyList<Loan>> GetActiveByUserAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cuenta los préstamos activos (Active u Overdue) de un usuario.
    /// Más eficiente que cargar la lista completa solo para contar.
    /// </summary>
    Task<int> CountActiveByUserAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene préstamos por estado específico.
    /// Útil para procesos automáticos (marcar vencidos, generar multas).
    /// </summary>
    Task<IReadOnlyList<Loan>> GetByStatusAsync(LoanStatus status, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los préstamos activos cuya fecha límite ya pasó.
    /// Se usa en el job diario de detección de préstamos vencidos.
    /// </summary>
    Task<IReadOnlyList<Loan>> GetOverdueLoansAsync(DateTime asOf, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el historial completo de préstamos de un usuario.
    /// </summary>
    Task<IReadOnlyList<Loan>> GetHistoryByUserAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene los préstamos activos de un libro en una sucursal específica.
    /// Se usa para saber cuántas copias están prestadas actualmente.
    /// </summary>
    Task<IReadOnlyList<Loan>> GetActiveByBookAndBranchAsync(Guid bookId, Guid branchId, CancellationToken cancellationToken = default);
}