using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.UnitTests.Domain.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Create_ValidAmount_ReturnsRoundedToTwoDecimals() // Comprueba que cualquier monto introducido se redondee automáticamente a dos decimales de forma correcta.
    {
        var money = Money.Create(10.555m);
        Assert.Equal(10.56m, money.Amount);
    }

    [Fact]
    public void Create_ZeroAmount_IsAllowed() // Comprueba que el sistema acepte valores en cero y marque correctamente la propiedad que identifica un monto nulo.
    {
        var money = Money.Create(0);
        Assert.True(money.IsZero);
    }

    [Fact]
    public void Create_NegativeAmount_Throws() // Comprueba que la aplicación bloquee la creación de dinero con signo negativo lanzando una excepción de dominio.
    {
        Assert.Throws<DomainValidationException>(() => Money.Create(-1));
    }

    [Fact]
    public void Zero_ReturnsZeroAmount() // Comprueba que la propiedad estática de conveniencia devuelva exactamente una instancia con valor numérico de cero.
    {
        Assert.Equal(0, Money.Zero.Amount);
    }

    [Fact]
    public void Add_TwoAmounts_ReturnsSum() // Comprueba que la operación matemática de suma combine correctamente los montos de dos instancias distintas.
    {
        var a = Money.Create(10);
        var b = Money.Create(5);
        Assert.Equal(15m, a.Add(b).Amount);
    }

    [Fact]
    public void Subtract_ValidAmount_ReturnsDifference() // Comprueba que la operación matemática de resta calcule correctamente la diferencia entre dos montos positivos.
    {
        var a = Money.Create(10);
        var b = Money.Create(3);
        Assert.Equal(7m, a.Subtract(b).Amount);
    }

    [Fact]
    public void Subtract_ResultIsNegative_Throws() // Comprueba que se impida realizar una resta si el sustraendo es mayor, evitando que el dinero final quede en negativo.
    {
        var a = Money.Create(5);
        var b = Money.Create(10);
        Assert.Throws<DomainValidationException>(() => a.Subtract(b));
    }

    [Fact]
    public void MultiplyBy_PositiveFactor_ReturnsProduct() // Comprueba que la multiplicación por un factor numérico calcule el producto exacto esperado del dinero.
    {
        var money = Money.Create(2.50m);
        Assert.Equal(7.50m, money.MultiplyBy(3).Amount);
    }

    [Fact]
    public void IsPositive_AboveZero_ReturnsTrue() // Comprueba que la propiedad de validación reconozca correctamente como positivo cualquier monto mayor a cero.
    {
        Assert.True(Money.Create(0.01m).IsPositive);
    }

    [Fact]
    public void IsPositive_Zero_ReturnsFalse() // Comprueba que el valor exacto de cero no sea considerado erróneamente como un monto estrictamente positivo.
    {
        Assert.False(Money.Zero.IsPositive);
    }

    // ── Igualdad y comparación ────────────────────────────────────────────
    [Fact]
    public void Equals_SameAmount_ReturnsTrue() // Comprueba que dos objetos de dinero se consideren idénticos si almacenan exactamente el mismo valor numérico.
    {
        Assert.Equal(Money.Create(5), Money.Create(5));
    }

    [Fact]
    public void Equals_DifferentAmount_ReturnsFalse() // Comprueba que el sistema diferencie de forma correcta dos instancias cuando sus montos económicos son distintos.
    {
        Assert.NotEqual(Money.Create(5), Money.Create(6));
    }

    [Fact]
    public void GreaterThan_Operator_Works() // Comprueba que el operador sobrecargado de "mayor que" (>) funcione correctamente para evaluar prioridades de precios.
    {
        Assert.True(Money.Create(10) > Money.Create(5));
    }

    [Fact]
    public void LessThan_Operator_Works() // Comprueba que el operador sobrecargado de "menor que" (<) determine correctamente cuál monto es de menor valor.
    {
        Assert.True(Money.Create(1) < Money.Create(5));
    }

    [Fact]
    public void ToString_ReturnsTwoDecimalFormat() // Comprueba que al transformar el objeto a texto plano devuelva la cadena formateada estrictamente con sus dos decimales.
    {
        Assert.Equal("10.00", Money.Create(10).ToString());
    }
}
