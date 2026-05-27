using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Repositorio de inventario de libros por sucursal.
/// Maneja las consultas de disponibilidad y ajuste de stock.
/// </summary>
public interface IBookStockRepository : IRepository<BookStock>
{
    /// <summary>
    /// Obtiene el stock de un libro en una sucursal específica.
    /// Retorna null si ese libro no está registrado en esa sucursal.
    /// </summary>
    Task<BookStock?> GetByBookAndBranchAsync(Guid bookId, Guid branchId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todo el inventario de un libro en todas las sucursales.
    /// Útil para mostrar disponibilidad global del catálogo.
    /// </summary>
    Task<IReadOnlyList<BookStock>> GetByBookAsync(Guid bookId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todo el inventario de una sucursal.
    /// Útil para reportes de inventario por sucursal.
    /// </summary>
    Task<IReadOnlyList<BookStock>> GetByBranchAsync(Guid branchId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si hay al menos una copia disponible de un libro en una sucursal.
    /// Más eficiente que cargar el objeto completo para solo verificar disponibilidad.
    /// </summary>
    Task<bool> HasAvailableCopiesAsync(Guid bookId, Guid branchId, CancellationToken cancellationToken = default);
}