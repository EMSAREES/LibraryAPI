using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.UnitTests.Domain.ValueObjects;

public class AddressTests
{
    [Fact]
    public void Create_ValidData_ReturnsAddressWithTrimmedValues() // Comprueba que una dirección válida se cree correctamente eliminando espacios innecesarios.
    {
        var address = Address.Create("  Main St 123  ", "  Springfield  ");

        Assert.Equal("Main St 123", address.Street);
        Assert.Equal("Springfield", address.City);
        Assert.Equal("Main St 123, Springfield", address.DisplayAddress);
    }

    [Theory]
    [InlineData("", "City")]
    [InlineData("   ", "City")]
    [InlineData(null, "City")]
    public void Create_EmptyStreet_Throws(string? street, string city) // Comprueba que no se permitan calles vacías, nulas o con solo espacios.
    {
        Assert.Throws<DomainValidationException>(() => Address.Create(street!, city));
    }

    [Theory]
    [InlineData("Street", "")]
    [InlineData("Street", "   ")]
    [InlineData("Street", null)]
    public void Create_EmptyCity_Throws(string street, string? city) // Comprueba que no se permitan ciudades vacías, nulas o con solo espacios.
    {
        Assert.Throws<DomainValidationException>(() => Address.Create(street, city!));
    }

    [Fact]
    public void Create_StreetTooLong_Throws() // Comprueba que se lance una excepción si la calle supera la longitud máxima permitida.
    {
        Assert.Throws<DomainValidationException>(() =>
            Address.Create(new string('A', 301), "City"));
    }

    [Fact]
    public void Create_CityTooLong_Throws() // Comprueba que se lance una excepción si la ciudad supera la longitud máxima permitida.
    {
        Assert.Throws<DomainValidationException>(() =>
            Address.Create("Street", new string('A', 101)));
    }

    [Fact]
    public void Equals_SameValues_ReturnsTrue() // Comprueba que dos direcciones con los mismos datos sean consideradas iguales.
    {
        var a = Address.Create("Main St", "City");
        var b = Address.Create("Main St", "City");

        Assert.Equal(a, b);
    }

    [Fact]
    public void Equals_DifferentCity_ReturnsFalse() // Comprueba que dos direcciones con ciudades distintas no sean consideradas iguales.
    {
        var a = Address.Create("Main St", "City A");
        var b = Address.Create("Main St", "City B");

        Assert.NotEqual(a, b);
    }
}