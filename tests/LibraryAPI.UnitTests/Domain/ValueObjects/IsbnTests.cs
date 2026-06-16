using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.UnitTests.Domain.ValueObjects;

public class IsbnTests
{
    // ── Camino feliz ──────────────────────────────────────────────────────
    [Theory]
    [InlineData("9780306406157")]          // ISBN-13 válido
    [InlineData("978-0-306-40615-7")]      // con guiones, se normaliza
    [InlineData("0306406152")]             // ISBN-10 válido
    public void Create_ValidIsbn_ReturnsNormalized(string input) // Comprueba que códigos ISBN correctos (10 o 13 dígitos) se creen bien, limpiando guiones y espacios.
    {
        var isbn = Isbn.Create(input);
        var normalized = input.Replace("-", "").Replace(" ", "").Trim();
        Assert.Equal(normalized, isbn.Value);
    }

    // ── Vacío / nulo ──────────────────────────────────────────────────────
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_NullOrEmpty_Throws(string? input) // Comprueba que el sistema rechace la creación si el código ISBN viene completamente vacío o nulo.
    {
        Assert.Throws<DomainValidationException>(() => Isbn.Create(input!));
    }

    // ── Longitud incorrecta ───────────────────────────────────────────────
    [Theory]
    [InlineData("12345")]           // demasiado corto
    [InlineData("12345678901234")]  // 14 dígitos
    public void Create_WrongLength_Throws(string input) // Comprueba que falle si la longitud no corresponde exactamente a las estructuras oficiales de 10 o 13 dígitos.
    {
        Assert.Throws<DomainValidationException>(() => Isbn.Create(input));
    }

    // ── Caracteres no numéricos ───────────────────────────────────────────
    [Fact]
    public void Create_NonDigitCharacters_Throws() // Comprueba que se bloquee el registro si el código contiene letras o caracteres extraños no permitidos.
    {
        Assert.Throws<DomainValidationException>(() => Isbn.Create("978030640615X"));
    }

    // ── Dígito de control ISBN-13 inválido ────────────────────────────────
    [Fact]
    public void Create_InvalidIsbn13CheckDigit_Throws() // Comprueba el algoritmo matemático interno rechazando el ISBN si el último dígito verificador es falso o incorrecto.
    {
        // Mismo ISBN-13 pero con dígito de control incorrecto (7 → 9)
        Assert.Throws<DomainValidationException>(() => Isbn.Create("9780306406159"));
    }

    // ── Igualdad ──────────────────────────────────────────────────────────
    [Fact]
    public void Equals_SameValue_ReturnsTrue() // Comprueba que dos objetos ISBN se consideren iguales si representan el mismo libro, aunque uno tenga guiones y el otro no.
    {
        var a = Isbn.Create("9780306406157");
        var b = Isbn.Create("978-0-306-40615-7");

        Assert.Equal(a, b);
    }

    [Fact]
    public void Equals_DifferentValue_ReturnsFalse() // Comprueba que el sistema distinga correctamente cuando se introducen dos códigos pertenecientes a libros diferentes.
    {
        var a = Isbn.Create("9780306406157");
        var b = Isbn.Create("0306406152");

        Assert.NotEqual(a, b);
    }

    // ── Conversión implícita ──────────────────────────────────────────────
    [Fact]
    public void ImplicitConversion_ReturnsValue() // Comprueba que se pueda extraer el valor de texto plano directamente asignando el objeto a una variable string.
    {
        var isbn = Isbn.Create("9780306406157");
        string result = isbn;
        Assert.Equal("9780306406157", result);
    }
}
