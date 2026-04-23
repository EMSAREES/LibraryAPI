using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Categories;

/// <summary>
/// Se lanza cuando se intenta eliminar una categoría que está asociada a libros.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class CategoryInUseException : DomainException
{
    public Guid CategoryId { get; }
    public int BooksCount { get; }

    public CategoryInUseException(Guid categoryId, int booksCount)
        : base("CATEGORY_IN_USE", DomainErrors.Category.CategoryInUse)
    {
        CategoryId = categoryId;
        BooksCount = booksCount;
    }
}
