using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Categories;

/// <summary>
/// Se lanza cuando se busca una categoría por ID y no existe en el sistema.
/// HTTP → 404 Not Found.
/// </summary>
public sealed class CategoryNotFoundException : DomainException
{
    public Guid CategoryId { get; }

    public CategoryNotFoundException(Guid categoryId)
        : base("CATEGORY_NOT_FOUND", DomainErrors.Category.CategoryNotFound)
    {
        CategoryId = categoryId;
    }
}
