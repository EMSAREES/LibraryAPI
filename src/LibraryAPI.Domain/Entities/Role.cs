using LibraryAPI.Domain.Enums;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;

namespace LibraryAPI.Domain.Entities;

public class Role : BaseEntity
{
    public UserRole Name { get; private set; }

    /// <summary>
    /// Descripción legible del rol y sus permisos generales.
    /// </summary>
    public string Description { get; private set; } = string.Empty;

    // Constructor privado para EF Core
    private Role() { }

    /// <summary>
    /// Constructor privado que inicializa un rol con nombre y descripción.
    /// Se usa únicamente desde el método de fábrica <see cref="Create"/> 
    /// para garantizar que la instancia se cree con validaciones de negocio.
    /// </summary>
    private Role(UserRole name, string description)
    {
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Crea un nuevo rol validando que la descripción no esté vacía.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si la descripción es nula o vacía.
    /// </exception>
    public static Role Create(UserRole name, string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(Description)
            };

        return new Role(name, description);
    }

}