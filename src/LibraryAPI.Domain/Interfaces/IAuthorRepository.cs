using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Repositorio de autores del catálogo.
/// </summary>
public interface IAuthorRepository : IRepository<Author>
{
    /// <summary>
    /// Verifica si ya existe un autor con el mismo nombre completo.
    /// Evita duplicados en el catálogo.
    /// </summary>
    Task<bool> ExistsByFullNameAsync(string firstName, string lastName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los autores con su lista de libros cargada.
    /// </summary>
    Task<IReadOnlyList<Author>> GetWithBooksAsync(CancellationToken cancellationToken = default);
}