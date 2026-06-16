using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.UnitTests.Domain.ValueObjects;

public class ImagenTests
{
    [Fact]
    public void Create_ValidData_ReturnsTrimmedImagen() // Comprueba que una imagen válida se cree correctamente eliminando espacios innecesarios en la URL y el texto alternativo.
    {
        var imagen = Imagen.Create(
            "  https://site.com/image.jpg  ",
            "  Portada del libro  ");

        Assert.Equal("https://site.com/image.jpg", imagen.Url);
        Assert.Equal("Portada del libro", imagen.AltText);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_EmptyUrl_Throws(string? url) // Comprueba que no se permitan URLs vacías, nulas o con solo espacios.
    {
        Assert.Throws<DomainValidationException>(() =>
            Imagen.Create(url!, "Alt text"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_EmptyAltText_Throws(string? altText) // Comprueba que no se permitan textos alternativos vacíos, nulos o con solo espacios.
    {
        Assert.Throws<DomainValidationException>(() =>
            Imagen.Create("https://site.com/image.jpg", altText!));
    }

    [Fact]
    public void Create_UrlTooLong_Throws() // Comprueba que se lance una excepción si la URL supera la longitud máxima permitida.
    {
        var longUrl = new string('A', 2001);

        Assert.Throws<DomainValidationException>(() =>
            Imagen.Create(longUrl, "Alt text"));
    }

    [Fact]
    public void Create_AltTextTooLong_Throws() // Comprueba que se lance una excepción si el texto alternativo supera la longitud máxima permitida.
    {
        var longAltText = new string('A', 301);

        Assert.Throws<DomainValidationException>(() =>
            Imagen.Create("https://site.com/image.jpg", longAltText));
    }

    [Fact]
    public void Equals_SameValues_ReturnsTrue() // Comprueba que dos imágenes con la misma URL y texto alternativo sean consideradas iguales.
    {
        var a = Imagen.Create(
            "https://site.com/image.jpg",
            "Portada");

        var b = Imagen.Create(
            "https://site.com/image.jpg",
            "Portada");

        Assert.Equal(a, b);
    }

    [Fact]
    public void Equals_DifferentValues_ReturnsFalse() // Comprueba que dos imágenes con datos diferentes no sean consideradas iguales.
    {
        var a = Imagen.Create(
            "https://site.com/image1.jpg",
            "Portada");

        var b = Imagen.Create(
            "https://site.com/image2.jpg",
            "Contraportada");

        Assert.NotEqual(a, b);
    }

    [Fact]
    public void GetHashCode_SameValues_ReturnsSameHash() // Comprueba que imágenes idénticas generen el mismo código hash para funcionar correctamente en colecciones.
    {
        var a = Imagen.Create(
            "https://site.com/image.jpg",
            "Portada");

        var b = Imagen.Create(
            "https://site.com/image.jpg",
            "Portada");

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void ToString_ReturnsFormattedString() // Comprueba que la representación en texto de la imagen incluya la URL y el texto alternativo.
    {
        var imagen = Imagen.Create(
            "https://site.com/image.jpg",
            "Portada");

        Assert.Equal(
            "https://site.com/image.jpg (Portada)",
            imagen.ToString());
    }
}