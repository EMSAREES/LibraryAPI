namespace LibraryAPI.Domain.Enums;

/// <summary>
/// Idioma en el que está escrito el libro.
/// </summary>
public enum BookLanguage
{
    /// <summary>
    /// Libro escrito en español.
    /// </summary>
    Spanish = 1,

    /// <summary>
    /// Libro escrito en inglés.
    /// </summary>
    English = 2,

    /// <summary>
    /// Libro escrito en francés.
    /// </summary>
    French = 3,

    /// <summary>
    /// Libro escrito en portugués.
    /// </summary>
    Portuguese = 4,

    /// <summary>
    /// Libro escrito en alemán.
    /// </summary>
    German = 5,

    /// <summary>
    /// Libro escrito en italiano.
    /// </summary>
    Italian = 6,

    /// <summary>
    /// Libro escrito en otro idioma no listado.
    /// </summary>
    Other = 99,
}