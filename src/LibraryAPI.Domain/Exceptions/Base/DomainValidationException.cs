namespace LibraryAPI.Domain.Exceptions.Base;

/// <summary>
/// Se lanza cuando un campo o argumento no cumple las reglas de validación
/// básicas del dominio: nulo, vacío, fuera de rango, formato inválido.
/// El middleware la convierte en HTTP 422 Unprocessable Entity.
/// </summary>
public sealed class DomainValidationException : DomainException
{
    public DomainValidationException(string message)
        : base("VALIDATION_ERROR", message) { }

    /// <summary>
    /// Permite indicar qué campo específico falló la validación.
    /// </summary>
    public string? FieldName { get; init; }
}