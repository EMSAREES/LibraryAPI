namespace LibraryAPI.Domain.Exceptions.Base;

/// <summary>
/// Excepción base para todas las violaciones de reglas de negocio del dominio.
/// Toda excepción de dominio debe heredar de esta clase.
/// El middleware de la API la intercepta y la convierte en HTTP 400 o 409.
/// </summary>
public abstract class DomainException : Exception
{
    /// <summary>
    /// Código legible de la excepción para identificarla en logs y respuestas API.
    /// Ejemplo: "LOAN_USER_BLOCKED", "BOOK_NOT_AVAILABLE".
    /// </summary>
    public string Code { get; }

    protected DomainException(string code, string message)
        : base(message)
    {
        Code = code;
    }
}