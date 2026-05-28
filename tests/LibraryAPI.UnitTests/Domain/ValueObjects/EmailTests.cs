using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.UnitTests.Domain.ValueObjects;

public class EmailTests
{
    // ── Create: camino feliz ──────────────────────────────────────────────
    [Theory]
    [InlineData("user@example.com")]
    [InlineData("  USER@EXAMPLE.COM  ")]   // normaliza espacios y mayúsculas
    [InlineData("user.name+tag@sub.domain.org")]
    public void Create_ValidEmail_ReturnsNormalizedLowercase(string input) // Comprueba que correos correctos se creen bien, se limpien de espacios y se pasen a minúsculas.
    {
        var email = Email.Create(input);

        Assert.Equal(input.Trim().ToLowerInvariant(), email.Value);
    }

    // ── Create: validaciones ──────────────────────────────────────────────
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_EmptyOrNull_ThrowsDomainValidationException(string? input) // Comprueba que el sistema rechace strings vacíos, nulos o con puros espacios lanzando un error.
    {
        Assert.Throws<DomainValidationException>(() => Email.Create(input!));
    }
 
    [Theory]
    [InlineData("notanemail")]
    [InlineData("missing@dot")]
    [InlineData("@nodomain.com")]
    [InlineData("nodomain@")]
    public void Create_InvalidFormat_ThrowsDomainValidationException(string input) // Comprueba que correos con estructuras incorrectas o sin puntos en el dominio sean bloqueados.
    {
        Assert.Throws<DomainValidationException>(() => Email.Create(input));
    }
 
    [Fact]
    public void Create_TooLong_ThrowsDomainValidationException() // Comprueba que se dispare un error si el correo excede el límite máximo permitido de 200 caracteres.
    {
        // 200 'a's + 6 caracteres del dominio = 206 caracteres totales (Supera los 200)
        var longEmail = new string('a', 200) + "@b.com"; 
        Assert.Throws<DomainValidationException>(() => Email.Create(longEmail));
    }


    // ── Igualdad ──────────────────────────────────────────────────────────
    [Fact]
    public void Equals_SameValue_ReturnsTrue() // Comprueba que dos objetos Email sean idénticos si su contenido es igual, ignorando mayúsculas/minúsculas originales.
    {
        var a = Email.Create("user@example.com");
        var b = Email.Create("USER@EXAMPLE.COM");

        Assert.Equal(a, b);
    }

    [Fact]
    public void Equals_DifferentValue_ReturnsFalse() // Comprueba que el sistema reconozca correctamente cuando dos correos electrónicos pertenecen a cuentas distintas.
    {
        var a = Email.Create("user@example.com");
        var b = Email.Create("other@example.com");

        Assert.NotEqual(a, b);
    }

    [Fact]
    public void GetHashCode_SameValue_SameHash() // Comprueba que correos idénticos generen el mismo código hash numérico para que funcionen bien en diccionarios y listas.
    {
        var a = Email.Create("user@example.com");
        var b = Email.Create("USER@EXAMPLE.COM");

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    // ── Conversión implícita ──────────────────────────────────────────────
    [Fact]
    public void ImplicitConversion_ToString_ReturnsValue() // Comprueba que se pueda asignar el objeto Email directamente a una variable de tipo string de forma automática.
    {
        var email = Email.Create("user@example.com");
        string result = email;
 
        Assert.Equal("user@example.com", result);
    }
}
