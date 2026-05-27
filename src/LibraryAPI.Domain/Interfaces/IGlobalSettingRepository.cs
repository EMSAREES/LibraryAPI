using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Enums;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Repositorio de configuraciones globales del sistema.
/// Las políticas (multa por día, límite de préstamos, etc.) se consultan
/// frecuentemente desde Application.
/// </summary>
public interface IGlobalSettingRepository : IRepository<GlobalSetting>
{
    /// <summary>
    /// Obtiene una configuración por su clave predefinida.
    /// Retorna null si la clave no ha sido configurada todavía.
    /// </summary>
    Task<GlobalSetting?> GetByKeyAsync(GlobalSettingKey key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si ya existe una configuración registrada para esa clave.
    /// </summary>
    Task<bool> ExistsByKeyAsync(GlobalSettingKey key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el valor de una clave directamente como string.
    /// Retorna null si no existe.
    /// Útil cuando solo se necesita el valor, no toda la entidad.
    /// </summary>
    Task<string?> GetValueAsync(GlobalSettingKey key, CancellationToken cancellationToken = default);
}