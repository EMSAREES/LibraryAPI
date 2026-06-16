using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.UnitTests.Domain.ValueObjects;

public class FullNameTests
{
    [Fact]
    public void Create_ValidNames_ReturnsTrimmedFullName() // Comprueba que nombres válidos se guarden bien, eliminando espacios basura y armando correctamente el nombre completo visible.
    {
        var fullName = FullName.Create("  John  ", "  Doe  ");

        Assert.Equal("John", fullName.FirstName);
        Assert.Equal("Doe", fullName.LastName);
        Assert.Equal("John Doe", fullName.DisplayName);
    }

    [Theory]
    [InlineData("", "Doe")]
    [InlineData("   ", "Doe")]
    [InlineData(null, "Doe")]
    public void Create_EmptyFirstName_Throws(string? first, string last) // Comprueba que el sistema rechace la creación del objeto si el primer nombre está vacío, nulo o tiene puros espacios.
    {
        Assert.Throws<DomainValidationException>(() => FullName.Create(first!, last));
    }

    [Theory]
    [InlineData("John", "")]
    [InlineData("John", "   ")]
    [InlineData("John", null)]
    public void Create_EmptyLastName_Throws(string first, string? last) // Comprueba que la aplicación bloquee el registro si el apellido está vacío, nulo o no contiene caracteres válidos.
    {
        Assert.Throws<DomainValidationException>(() => FullName.Create(first, last!));
    }

    [Fact]
    public void Create_FirstNameTooLong_Throws() // Comprueba que se dispare un error de validación si el primer nombre supera el límite máximo de caracteres permitido (100).
    {
        var longName = new string('A', 101);
        Assert.Throws<DomainValidationException>(() => FullName.Create(longName, "Doe"));
    }

    [Fact]
    public void Create_LastNameTooLong_Throws() // Comprueba que se lance una excepción si el apellido es demasiado largo y excede el límite máximo de caracteres establecido.
    {
        var longName = new string('A', 101);
        Assert.Throws<DomainValidationException>(() => FullName.Create("John", longName));
    }

    [Fact]
    public void Equals_SameNames_ReturnsTrue() // Comprueba que dos objetos FullName sean tratados como idénticos si tanto el nombre como el apellido coinciden de forma exacta.
    {
        var a = FullName.Create("John", "Doe");
        var b = FullName.Create("John", "Doe");

        Assert.Equal(a, b);
    }

    [Fact]
    public void Equals_DifferentNames_ReturnsFalse() // Comprueba que el sistema diferencie correctamente a dos personas si cambia el nombre o el apellido dentro del objeto de valor.
    {
        var a = FullName.Create("John", "Doe");
        var b = FullName.Create("Jane", "Doe");

        Assert.NotEqual(a, b);
    }

    [Fact]
    public void ToString_ReturnsDisplayName() // Comprueba que al formatear el objeto como texto (ToString) devuelva directamente el nombre completo combinado ("Nombre Apellido").
    {
        var fullName = FullName.Create("John", "Doe");
        Assert.Equal("John Doe", fullName.ToString());
    }
}
