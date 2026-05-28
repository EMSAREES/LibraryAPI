using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace LibraryAPI.Domain.ValueObjects;

/// <summary>
/// Representa un correo electrónico válido dentro del dominio.
/// Garantiza formato correcto en el momento de su creación.
/// Se usa en User para evitar strings sin validar.
/// </summary>
public sealed class Email : IEquatable<Email>
{
    /// <summary>
    /// Valor del correo normalizado a minúsculas y sin espacios.
    /// </summary>
    public string Value { get; }

    private Email(string value) => Value = value;
    

    // Regex ultra rápido para asegurar que el bloque del dominio tenga al menos un punto intermedio
    private static readonly Regex DomainDotRegex = new(@"@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    /// <summary>
    /// Crea una instancia de Email validando formato antes de retornar.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si el correo es nulo, vacío o tiene formato inválido.
    /// </exception>
    public static Email Create(string value)
    {
        // 1. Validar nulidad o vacío primero
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(Email)
            };

        // 2. Validar longitud antes de normalizar (Protección contra strings masivos en memoria)
        if (value.Length > 200)
            throw new DomainValidationException(DomainErrors.Validation.StringTooLong)
            {
                FieldName = nameof(Email)
            };

        var normalized = value.Trim().ToLowerInvariant(); // eliminamos los espasio en blanco

        // 3. Validación robusta de formato usando la librería nativa de .NET
        try
        {
            // Intenta parsear el correo. Si falla, saltará al bloque catch.
            _ = new MailAddress(normalized);

             //  Fuerza que el dominio contenga un punto obligatorio (Ej: evitamos "missing@dot")
            if (!DomainDotRegex.IsMatch(normalized))
            {
                throw new FormatException();
            }
        }
        catch (FormatException)
        {
            throw new DomainValidationException("El formato del correo electrónico no es válido.")
            {
                FieldName = nameof(Email)
            };
        }

        return new Email(normalized);
    }


    /// <summary>
    /// Compara esta instancia de Email con otra, verificando que no sea nula
    /// y que ambas tengan el mismo valor normalizado.
    /// </summary>
    public bool Equals(Email? other) => other is not null && Value == other.Value;

    /// <summary>
    /// Sobrescribe Equals para permitir comparación con objetos genéricos.
    /// Si el objeto es un Email, delega la comparación al método Equals específico.
    /// </summary>
    public override bool Equals(object? obj) => obj is Email other && Equals(other);

    /// <summary>
    /// Genera un código hash basado en el valor del correo.
    /// Se usa en colecciones como diccionarios o conjuntos para identificar instancias únicas.
    /// </summary>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Devuelve el valor del correo como cadena.
    /// Útil para mostrar el Email directamente en logs o interfaces.
    /// </summary>
    public override string ToString() => Value;

    /// <summary>
    /// Conversión implícita de Email a string.
    /// Permite asignar un Email directamente a una variable string sin necesidad de llamar a .Value.
    /// </summary>
    public static implicit operator string(Email email) => email.Value;

}