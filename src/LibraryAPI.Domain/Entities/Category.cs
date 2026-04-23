using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Exceptions.Categories;
using LibraryAPI.Domain.Events.Categories;

namespace LibraryAPI.Domain.Entities;

public class Category : BaseEntity
{
    
    public string Name { get; private set; } = string.Empty;
    /// <summary>
    /// Descripción opcional de la categoría.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Libros asociados a esta categoría.
    /// </summary>
    public IReadOnlyList<BookCategory> Books { get; private set; } = [];

    // Constructor privado para EF Core
    private Category() { }

    private Category(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Crea una nueva categoría validando que el nombre no esté vacío.
    /// </summary>
    /// <exception cref="InvalidCategoryDataException">
    /// Se lanza si el nombre es nulo, vacío o excede el largo máximo.
    /// </exception>
    public static Category Create(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidCategoryDataException(DomainErrors.Validation.ValueRequired);

        if (name.Trim().Length > 100)
            throw new InvalidCategoryDataException(DomainErrors.Validation.StringTooLong);

        var category = new Category(name.Trim(), description?.Trim());
        category.AddDomainEvent(new CategoryCreatedEvent(category));
        return category;
    }

    /// <summary>
    /// Actualiza el nombre y descripción de la categoría.
    /// </summary>
    public void Update(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidCategoryDataException(DomainErrors.Validation.ValueRequired);

        Name = name.Trim();
        Description = description?.Trim();

        MarkAsUpdated();
        AddDomainEvent(new CategoryUpdatedEvent(this));
    }
}