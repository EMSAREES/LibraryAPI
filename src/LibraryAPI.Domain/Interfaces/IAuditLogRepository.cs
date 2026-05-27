using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Enums;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Repositorio de logs de auditoría.
/// Solo soporta escritura y consulta — nunca actualización ni eliminación.
/// </summary>
public interface IAuditLogRepository
{
    /// <summary>
    /// Registra un nuevo log de auditoría.
    /// </summary>
    Task AddAsync(AuditLogs auditLog, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el historial de acciones realizadas sobre una entidad específica.
    /// </summary>
    Task<IReadOnlyList<AuditLogs>> GetByEntityAsync(string entityType, Guid entityId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene las acciones realizadas por un usuario específico.
    /// </summary>
    Task<IReadOnlyList<AuditLogs>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene logs filtrados por tipo de acción en un rango de fechas.
    /// Útil para reportes y revisiones administrativas.
    /// </summary>
    Task<IReadOnlyList<AuditLogs>> GetByActionAsync(AuditAction action, DateTime from, DateTime to, CancellationToken cancellationToken = default);
}