using LibraryAPI.Domain.Enums;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Events.Books;

namespace LibraryAPI.Domain.Entities;

/// <summary>
/// Tabla pivot que relaciona un libro con sus categorías temáticas.
/// Permite que un libro pertenezca a múltiples categorías.
/// Ejemplo: un libro puede ser "Tecnología" y "Educación" al mismo tiempo.
/// </summary>
public sealed class BookCategory : BaseEntity
{
    /// <summary>
    /// Identificador del libro.
    /// </summary>
    public Guid BookId { get; private set; }

    /// <summary>
    /// Identificador de la categoría.
    /// </summary>
    public Guid CategoryId { get; private set; }

    /// <summary>
    /// Navegación al libro relacionado.
    /// </summary>
    public Book Book { get; private set; } = null!;

    /// <summary>
    /// Navegación a la categoría relacionada.
    /// </summary>
    public Category Category { get; private set; } = null!;

    // Constructor privado para EF Core
    private BookCategory() { }

    private BookCategory(Guid bookId, Guid categoryId)
    {
        BookId = bookId;
        CategoryId = categoryId;
    }

    /// <summary>
    /// Crea una nueva relación entre un libro y una categoría.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si alguno de los identificadores es inválido.
    /// </exception>
    public static BookCategory Create(Guid bookId, Guid categoryId)
    {
        if (bookId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(BookId)
            };

        if (categoryId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(CategoryId)
            };

        var bookCategory = new BookCategory(bookId, categoryId);
        bookCategory.AddDomainEvent(new BookCategoryCreatedEvent(bookCategory));
        return bookCategory;
    }
}