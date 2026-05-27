using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Repositorio de categorías de libros.
/// </summary>
public interface ICategoryRepository : IRepository<Category>
{
    /// <summary>
    /// Verifica si ya existe una categoría con ese nombre (case-insensitive).
    /// </summary>
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cuenta cuántos libros están asociados a una categoría.
    /// Se usa antes de eliminarla para evitar lanzar CategoryInUseException
    /// sin consultar la base de datos innecesariamente.
    /// </summary>
    Task<int> CountBooksByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
}