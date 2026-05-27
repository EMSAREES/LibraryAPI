using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Repositorio de sucursales.
/// </summary>
public interface IBranchRepository : IRepository<Branch>
{
    /// <summary>
    /// Obtiene solo las sucursales activas.
    /// Se usa al mostrar opciones de sucursal al crear un préstamo o reserva.
    /// </summary>
    Task<IReadOnlyList<Branch>> GetActiveBranchesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe una sucursal con el nombre dado.
    /// Evita registrar sucursales duplicadas.
    /// </summary>
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
}