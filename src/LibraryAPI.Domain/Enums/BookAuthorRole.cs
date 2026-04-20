namespace LibraryAPI.Domain.Enums;

/// <summary>
/// Define el tipo de participación de un autor en un libro.
/// </summary>
public enum BookAuthorRole
{
    /// <summary>
    /// Es el autor principal o único del libro.
    /// </summary>
    MainAuthor = 1,

    /// <summary>
    /// Contribuyó al libro junto con otro autor principal.
    /// </summary>
    CoAuthor = 2,

    /// <summary>
    /// Realizó la traducción del libro a otro idioma.
    /// </summary>
    Translator = 3,

    /// <summary>
    /// Compiló o editó el contenido del libro.
    /// </summary>
    Editor = 4,

    /// <summary>
    /// Escribió el prólogo, prefacio o introducción del libro.
    /// </summary>
    Prologuist = 5,
}