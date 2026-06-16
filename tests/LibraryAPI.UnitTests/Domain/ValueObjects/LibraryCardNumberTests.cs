using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.UnitTests.Domain.ValueObjects;

public class LibraryCardNumberTests
{
    [Fact]
    public void Create_ValidValue_Normalizes_ToUppercase() // Comprueba que un número de tarjeta válido se normalice eliminando espacios y convirtiéndose a mayúsculas.
    {
        var card = LibraryCardNumber.Create("  lc-abc123  ");

        Assert.Equal("LC-ABC123", card.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_NullOrEmpty_Throws(string? input) // Comprueba que no se permitan números de tarjeta vacíos, nulos o con solo espacios.
    {
        Assert.Throws<DomainValidationException>(() => LibraryCardNumber.Create(input!));
    }

    [Fact]
    public void Create_TooLong_Throws() // Comprueba que se lance una excepción si el número de tarjeta supera la longitud máxima permitida.
    {
        Assert.Throws<DomainValidationException>(() =>
            LibraryCardNumber.Create(new string('A', 33)));
    }

    [Fact]
    public void Generate_ReturnsCardWithLcPrefix() // Comprueba que las tarjetas generadas automáticamente comiencen con el prefijo obligatorio "LC-".
    {
        var card = LibraryCardNumber.Generate();

        Assert.StartsWith("LC-", card.Value);
    }

    [Fact]
    public void Generate_ReturnsDifferentValues_EachTime() // Comprueba que cada tarjeta generada tenga un valor distinto para evitar duplicados.
    {
        var a = LibraryCardNumber.Generate();
        var b = LibraryCardNumber.Generate();

        // No es determinista — con alta probabilidad son distintos
        Assert.NotEqual(a, b);
    }

    [Fact]
    public void Generate_ValueLength_Is19Chars() // Comprueba que las tarjetas generadas tengan exactamente 19 caracteres.
    {
        // "LC-" (3) + 16 chars = 19
        var card = LibraryCardNumber.Generate();

        Assert.Equal(19, card.Value.Length);
    }

    [Fact]
    public void Equals_SameValue_ReturnsTrue() // Comprueba que dos tarjetas con el mismo valor sean consideradas iguales aunque cambien las mayúsculas/minúsculas originales.
    {
        var a = LibraryCardNumber.Create("LC-ABC123");
        var b = LibraryCardNumber.Create("lc-abc123");

        Assert.Equal(a, b);
    }

    [Fact]
    public void ImplicitConversion_ReturnsValue() // Comprueba que el objeto LibraryCardNumber pueda convertirse automáticamente a string devolviendo su valor interno.
    {
        var card = LibraryCardNumber.Create("LC-TEST");

        string result = card;

        Assert.Equal("LC-TEST", result);
    }
}