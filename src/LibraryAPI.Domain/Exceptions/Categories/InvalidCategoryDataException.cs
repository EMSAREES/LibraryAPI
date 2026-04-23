using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Categories;

/// <summary>
/// Se lanza cuando los datos de la categoría no cumplen con las reglas de negocio.
/// Por ejemplo: nombre vacío, nombre muy largo, etc.
/// HTTP → 400 Bad Request.
/// </summary>
public sealed class InvalidCategoryDataException : DomainException
{
    public string? FieldName { get; set; }

    public InvalidCategoryDataException(string message)
        : base("INVALID_CATEGORY_DATA", message)
    {
    }
}
