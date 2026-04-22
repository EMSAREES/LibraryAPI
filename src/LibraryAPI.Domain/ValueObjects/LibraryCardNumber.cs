using System.Security.Cryptography;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;

namespace LibraryAPI.Domain.ValueObjects;

/// <summary>
/// Identificador de tarjeta de biblioteca asignada a un cliente.
/// Se usa para validar que el cliente presente su tarjeta al retirar libros,
/// sin necesidad de contraseña.
/// </summary>
public sealed class LibraryCardNumber : IEquatable<LibraryCardNumber>
{
    public string Value { get; }

    private LibraryCardNumber(string value) => Value = value;

    public static LibraryCardNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(LibraryCardNumber)
            };

        var normalized = value.Trim().ToUpperInvariant();

        if (normalized.Length > 32)
            throw new DomainValidationException(DomainErrors.Validation.StringTooLong)
            {
                FieldName = nameof(LibraryCardNumber)
            };

        return new LibraryCardNumber(normalized);
    }

    /// <summary>
    /// Genera un número de tarjeta pseudo-aleatorio apto para imprimir como QR / código de barras.
    /// Formato: "LC-" + 16 chars (Base32 sin caracteres ambiguos).
    /// </summary>
    public static LibraryCardNumber Generate()
    {
        Span<byte> bytes = stackalloc byte[10];
        RandomNumberGenerator.Fill(bytes);

        const string alphabet = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ"; // sin 0/1/I/O
        Span<char> buf = stackalloc char[16];

        for (var i = 0; i < buf.Length; i++)
        {
            buf[i] = alphabet[bytes[i % bytes.Length] % alphabet.Length];
        }

        return new LibraryCardNumber($"LC-{new string(buf)}");
    }

    public bool Equals(LibraryCardNumber? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is LibraryCardNumber other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static implicit operator string(LibraryCardNumber number) => number.Value;
}

