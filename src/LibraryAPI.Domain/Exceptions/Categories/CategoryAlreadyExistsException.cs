using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Categories;

/// <summary>
/// Se lanza cuando se intenta crear una categoría con un nombre que ya existe en el sistema.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class CategoryAlreadyExistsException : DomainException
{
    public string CategoryName { get; }

    public CategoryAlreadyExistsException(string categoryName)
        : base("CATEGORY_ALREADY_EXISTS", DomainErrors.Category.CategoryAlreadyExists)
    {
        CategoryName = categoryName;
    }
}
