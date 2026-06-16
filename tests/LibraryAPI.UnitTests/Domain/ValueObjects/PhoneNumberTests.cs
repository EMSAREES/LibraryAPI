using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.UnitTests.Domain.ValueObjects;

public class PhoneNumberTests
{
    [Theory]
    [InlineData("+1 (555) 123-4567")]
    [InlineData("5551234567")]
    [InlineData("+52-55-1234-5678")]
    public void Create_ValidNumber_ReturnsPhoneNumber(string input) // Comprueba que números telefónicos válidos se creen correctamente conservando el formato ingresado.
    {
        var phone = PhoneNumber.Create(input);

        Assert.Equal(input.Trim(), phone.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_NullOrEmpty_Throws(string? input) // Comprueba que no se permitan números telefónicos vacíos, nulos o con solo espacios.
    {
        Assert.Throws<DomainValidationException>(() => PhoneNumber.Create(input!));
    }

    [Fact]
    public void Create_TooLong_Throws() // Comprueba que se lance una excepción si el número telefónico supera la longitud máxima permitida.
    {
        Assert.Throws<DomainValidationException>(() =>
            PhoneNumber.Create(new string('1', 21)));
    }

    [Theory]
    [InlineData("555abc1234")]
    [InlineData("555@1234")]
    public void Create_InvalidCharacters_Throws(string input) // Comprueba que se rechacen números telefónicos con letras o caracteres inválidos.
    {
        Assert.Throws<DomainValidationException>(() => PhoneNumber.Create(input));
    }

    [Fact]
    public void Equals_SameValue_ReturnsTrue() // Comprueba que dos números telefónicos con el mismo valor sean considerados iguales.
    {
        var a = PhoneNumber.Create("+1-555-1234");
        var b = PhoneNumber.Create("+1-555-1234");

        Assert.Equal(a, b);
    }

    [Fact]
    public void ImplicitConversion_ReturnsValue() // Comprueba que el objeto PhoneNumber pueda convertirse automáticamente a string devolviendo su valor interno.
    {
        var phone = PhoneNumber.Create("5551234");

        string result = phone;

        Assert.Equal("5551234", result);
    }
}