namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Abstracción de la unidad de trabajo.
/// Agrupa todas las operaciones pendientes de repositorios y las
/// persiste en una sola transacción atómica contra PostgreSQL.
/// La implementación real usa DbContext.SaveChangesAsync de EF Core.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Persiste todos los cambios pendientes en la base de datos.
    /// También se encarga de publicar los domain events acumulados
    /// en las entidades antes (o después) de guardar, según el interceptor.
    /// </summary>
    /// <returns>Número de filas afectadas.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Inicia una transacción explícita para operaciones que requieren
    /// atomicidad entre múltiples agregados (ej. crear préstamo + decrementar stock).
    /// </summary>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Confirma la transacción activa.
    /// </summary>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Revierte la transacción activa en caso de error.
    /// </summary>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}