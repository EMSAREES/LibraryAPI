using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Contrato genérico para repositorios de entidades del dominio.
/// La implementación real vive en Infrastructure (EF Core + PostgreSQL).
/// Application solo depende de esta abstracción.
/// </summary>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Obtiene una entidad por su identificador único.
    /// Retorna null si no existe.
    /// </summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todas las entidades de este tipo.
    /// Úsalo solo en catálogos pequeños (roles, categorías).
    /// </summary>
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Agrega una nueva entidad al contexto (pendiente de guardar).
    /// </summary>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Marca la entidad como modificada en el contexto.
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Marca la entidad como eliminada en el contexto.
    /// </summary>
    void Delete(T entity);

    /// <summary>
    /// Verifica si existe una entidad con el ID dado.
    /// </summary>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}