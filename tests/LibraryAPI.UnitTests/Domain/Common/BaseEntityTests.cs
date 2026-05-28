using LibraryAPI.Domain.Common;
using Xunit;

namespace LibraryAPI.UnitTests.Domain.Common;

public class BaseEntityTests
{
    // ── Propiedades Iniciales ─────────────────────────────────────────────
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly() // Verifica que al instanciar una entidad se generen IDs y fechas por defecto.
    {
        // Act
        var entity = new TestEntity();

        // Assert
        Assert.NotEqual(Guid.Empty, entity.Id);
        Assert.True(entity.CreatedAt <= DateTime.UtcNow);

    }

    // ── Clases Falsas (Stubs) para dar soporte al Test ───────────────────
    
    // Entidad ficticia para probar los métodos protegidos y abstractos de BaseEntity
    private class TestEntity : BaseEntity
    {
        // Expone el método protegido AddDomainEvent hacia la prueba
        public void RaiseEvent(IDomainEvent domainEvent) => AddDomainEvent(domainEvent);

        // Expone el método protegido MarkAsUpdated hacia la prueba
        public void TriggerUpdate() => MarkAsUpdated();
    }

    // Evento ficticio para poblar la lista IDomainEvent
    private class TestDomainEvent : BaseDomainEvent { }

}
