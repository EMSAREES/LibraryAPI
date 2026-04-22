using LibraryAPI.Domain.Enums;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;

using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.Domain.Entities;

public class Author : BaseEntity
{
     /// <summary>
    /// Nombre completo del autor.
    /// </summary>
    public FullName FullName { get; private set; } = null!;

    public string? Nationality { get; private set; }

    /// <summary>
    /// Fecha de nacimiento del autor. Opcional.
    /// </summary>
    public DateOnly? BirthDate { get; private set; }
    
    /// <summary>
    /// Biografía breve del autor. Opcional.
    /// </summary>
    public string? Biography { get; private set; }

    /// <summary>
    /// Libros asociados a este autor.
    /// </summary>
    public IReadOnlyList<BookAuthor> Books { get; private set; } = [];

    // Constructor privado para EF Core
    private Author() { }

    private Author(
        FullName fullName,
        string? nationality,
        DateOnly? birthDate,
        string? biography,
        Guid createdByUserId)
    {
        FullName = fullName;
        Nationality = nationality;
        BirthDate = birthDate;
        Biography = biography;
        CreatedByUserId = createdByUserId;
    }

    /// <summary>
    /// Crea un nuevo autor validando que tenga nombre completo.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si el nombre completo es nulo.
    /// </exception>
    public static Author Create(
        FullName fullName,
        string? nationality,
        DateOnly? birthDate,
        string? biography,
        Guid createdByUserId)
    {
        if (fullName is null)
            throw new DomainValidationException(DomainErrors.General.RequiredFieldNull)
            {
                FieldName = nameof(FullName)
            };

        if (createdByUserId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(createdByUserId)
            };

        return new Author(fullName, nationality, birthDate, biography, createdByUserId);
    }

    /// <summary>
    /// Actualiza los datos del autor.
    /// </summary>
    public void Update(
        FullName fullName,
        string? nationality,
        DateOnly? birthDate,
        string? biography)
    {
        if (fullName is null)
            throw new DomainValidationException(DomainErrors.General.RequiredFieldNull)
            {
                FieldName = nameof(FullName)
            };

        FullName = fullName;
        Nationality = nationality;
        BirthDate = birthDate;
        Biography = biography;

        MarkAsUpdated();
    }
}