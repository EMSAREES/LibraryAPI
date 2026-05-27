using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Enums;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Repositorio de reservas. Gestiona el ciclo de vida completo
/// desde Pending hasta Fulfilled, Cancelled o Expired.
/// </summary>
public interface IReservationRepository : IRepository<Reservation>
{
    /// <summary>
    /// Verifica si un usuario ya tiene una reserva activa (Pending o Notified)
    /// para un libro específico en una sucursal.
    /// Previene reservas duplicadas.
    /// </summary>
    Task<bool> HasActivePendingReservationAsync(Guid userId, Guid bookId, Guid branchId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene las reservas pendientes de un libro en una sucursal,
    /// ordenadas por fecha de creación (FIFO).
    /// Se usa cuando un libro es devuelto para notificar al siguiente en cola.
    /// </summary>
    Task<IReadOnlyList<Reservation>> GetPendingByBookAndBranchAsync(Guid bookId, Guid branchId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todas las reservas en estado Notified cuya fecha de expiración
    /// ya pasó. Se usa en el job diario de limpieza de reservas.
    /// </summary>
    Task<IReadOnlyList<Reservation>> GetExpiredNotificationsAsync(DateTime asOf, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el historial de reservas de un usuario.
    /// </summary>
    Task<IReadOnlyList<Reservation>> GetHistoryByUserAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene reservas por estado específico.
    /// </summary>
    Task<IReadOnlyList<Reservation>> GetByStatusAsync(ReservationStatus status, CancellationToken cancellationToken = default);
}