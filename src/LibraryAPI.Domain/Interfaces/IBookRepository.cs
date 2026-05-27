using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Repositorio de libros. Extiende el contrato genérico con
/// consultas específicas del catálogo.
/// </summary>
public interface IBookRepository : IRepository<Book>
{
    /// <summary>
    /// Busca un libro por su ISBN normalizado.
    /// Retorna null si no existe en el catálogo.
    /// </summary>
    Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si ya existe un libro con ese ISBN en el catálogo.
    /// </summary>
    Task<bool> ExistsByIsbnAsync(string isbn, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los libros activos del catálogo.
    /// </summary>
    Task<IReadOnlyList<Book>> GetActiveBooksAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene libros que pertenecen a una categoría específica.
    /// </summary>
    Task<IReadOnlyList<Book>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene libros que tienen al menos un autor específico.
    /// </summary>
    Task<IReadOnlyList<Book>> GetByAuthorAsync(Guid authorId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el libro con sus autores y categorías cargados (eager loading).
    /// Útil para mostrar el detalle completo de un libro.
    /// </summary>
    Task<Book?> GetWithDetailsAsync(Guid bookId, CancellationToken cancellationToken = default);
}