using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Events.Users;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Exceptions.Users;
using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.UnitTests.Domain.Entities;

public class UserTests
{
    // ── Helpers ───────────────────────────────────────────────────────────
    private static readonly Guid ValidRoleId = Guid.NewGuid();
    private static readonly Guid CreatedBy = Guid.NewGuid();

    private static readonly FullName ValidName =
        FullName.Create("John", "Doe");

    // Ahora el email es opcional
    private static readonly Email? ValidEmail =
        Email.Create("john@example.com");

    // Ahora el teléfono es obligatorio
    private static readonly PhoneNumber ValidPhone =
        PhoneNumber.Create("+52 5551234567");

    private static User BuildUser() =>
        User.Create(
            ValidName,
            ValidEmail,
            "hash123",
            ValidPhone,
            ValidRoleId,
            null,
            CreatedBy);

    // ── Create ────────────────────────────────────────────────────────────

    [Fact]
    public void Create_ValidData_ReturnsActiveUnblockedUser() // Comprueba que un usuario válido se cree activo, no bloqueado y con los datos correctos.
    {
        var user = BuildUser();

        Assert.True(user.IsActive);
        Assert.False(user.IsBlocked);
        Assert.Equal(ValidEmail, user.Email);
        Assert.Equal(ValidPhone, user.Phone);
    }

    [Fact]
    public void Create_WithoutEmail_AllowsNullEmail() // Comprueba que el correo sea opcional y pueda crearse un usuario con Email null.
    {
        var user = User.Create(
            ValidName,
            null,
            "hash123",
            ValidPhone,
            ValidRoleId,
            null,
            CreatedBy);

        Assert.Null(user.Email);
    }

    [Fact]
    public void Create_EmitsUserCreatedEvent() // Comprueba que al crear un usuario se genere el evento UserCreatedEvent.
    {
        var user = BuildUser();

        Assert.Single(user.DomainEvents.OfType<UserCreatedEvent>());
    }

    [Fact]
    public void Create_NullFullName_Throws() // Comprueba que no se permita crear un usuario sin nombre completo.
    {
        Assert.Throws<DomainValidationException>(() =>
            User.Create(
                null!,
                ValidEmail,
                "hash",
                ValidPhone,
                ValidRoleId,
                null,
                CreatedBy));
    }

    [Fact]
    public void Create_NullPhone_Throws() // Comprueba que no se permita crear un usuario sin número telefónico.
    {
        Assert.Throws<DomainValidationException>(() =>
            User.Create(
                ValidName,
                ValidEmail,
                "hash",
                null!,
                ValidRoleId,
                null,
                CreatedBy));
    }

    [Fact]
    public void Create_EmptyPasswordHash_Throws() // Comprueba que no se permita crear un usuario con contraseña vacía.
    {
        Assert.Throws<DomainValidationException>(() =>
            User.Create(
                ValidName,
                ValidEmail,
                "",
                ValidPhone,
                ValidRoleId,
                null,
                CreatedBy));
    }

    [Fact]
    public void Create_EmptyRoleId_Throws() // Comprueba que no se permita crear un usuario con un RoleId vacío.
    {
        Assert.Throws<DomainValidationException>(() =>
            User.Create(
                ValidName,
                ValidEmail,
                "hash",
                ValidPhone,
                Guid.Empty,
                null,
                CreatedBy));
    }

    // ── Block / Unblock ───────────────────────────────────────────────────

    [Fact]
    public void Block_ActiveUser_SetsIsBlockedTrue() // Comprueba que un usuario activo pueda ser bloqueado correctamente.
    {
        var user = BuildUser();

        user.Block();

        Assert.True(user.IsBlocked);
    }

    [Fact]
    public void Block_EmitsUserBlockedEvent() // Comprueba que al bloquear un usuario se genere el evento UserBlockedEvent.
    {
        var user = BuildUser();

        user.Block();

        Assert.Contains(user.DomainEvents, e => e is UserBlockedEvent);
    }

    [Fact]
    public void Block_AlreadyBlocked_Throws() // Comprueba que no se pueda bloquear un usuario que ya está bloqueado.
    {
        var user = BuildUser();

        user.Block();

        Assert.Throws<UserAlreadyBlockedException>(() => user.Block());
    }

    [Fact]
    public void Unblock_BlockedUser_SetsIsBlockedFalse() // Comprueba que un usuario bloqueado pueda desbloquearse correctamente.
    {
        var user = BuildUser();

        user.Block();
        user.Unblock();

        Assert.False(user.IsBlocked);
    }

    [Fact]
    public void Unblock_EmitsUserUnblockedEvent() // Comprueba que al desbloquear un usuario se genere el evento UserUnblockedEvent.
    {
        var user = BuildUser();

        user.Block();
        user.ClearDomainEvents();

        user.Unblock();

        Assert.Single(user.DomainEvents.OfType<UserUnblockedEvent>());
    }

    [Fact]
    public void Unblock_NotBlocked_Throws() // Comprueba que no se pueda desbloquear un usuario que no está bloqueado.
    {
        var user = BuildUser();

        Assert.Throws<UserNotBlockedException>(() => user.Unblock());
    }

    // ── UpdateProfile ─────────────────────────────────────────────────────

    [Fact]
    public void UpdateProfile_ValidData_UpdatesFieldsAndEmitsEvent() // Comprueba que actualizar el perfil modifique los datos y genere un evento de actualización.
    {
        var user = BuildUser();

        var newName = FullName.Create("Jane", "Doe");
        var newPhone = PhoneNumber.Create("+52 9999999999");

        user.UpdateProfile(newName, newPhone);

        Assert.Equal(newName, user.FullName);
        Assert.Equal(newPhone, user.Phone);

        Assert.Contains(user.DomainEvents, e => e is UserUpdatedEvent);
    }

    [Fact]
    public void UpdateProfile_NullFullName_Throws() // Comprueba que no se permita actualizar el perfil con un nombre nulo.
    {
        var user = BuildUser();

        Assert.Throws<DomainValidationException>(() =>
            user.UpdateProfile(null!, ValidPhone));
    }

    [Fact]
    public void UpdateProfile_NullPhone_Throws() // Comprueba que no se permita actualizar el perfil con teléfono nulo.
    {
        var user = BuildUser();

        Assert.Throws<DomainValidationException>(() =>
            user.UpdateProfile(ValidName, null!));
    }

    // ── UpdatePassword ────────────────────────────────────────────────────

    [Fact]
    public void UpdatePassword_ValidHash_UpdatesPassword() // Comprueba que la contraseña se actualice correctamente con un hash válido.
    {
        var user = BuildUser();

        user.UpdatePassword("newHash999");

        Assert.Equal("newHash999", user.PasswordHash);
    }

    [Fact]
    public void UpdatePassword_Empty_Throws() // Comprueba que no se permita actualizar la contraseña con un valor vacío.
    {
        var user = BuildUser();

        Assert.Throws<DomainValidationException>(() =>
            user.UpdatePassword(""));
    }

    // ── AssignBranch ──────────────────────────────────────────────────────

    [Fact]
    public void AssignBranch_ValidId_SetsBranchId() // Comprueba que una sucursal válida pueda asignarse correctamente al usuario.
    {
        var user = BuildUser();
        var branchId = Guid.NewGuid();

        user.AssignBranch(branchId);

        Assert.Equal(branchId, user.BranchId);
    }

    [Fact]
    public void AssignBranch_EmptyGuid_Throws() // Comprueba que no se permita asignar una sucursal con Guid vacío.
    {
        var user = BuildUser();

        Assert.Throws<DomainValidationException>(() =>
            user.AssignBranch(Guid.Empty));
    }

    // ── EnsureCanBorrow ───────────────────────────────────────────────────

    [Fact]
    public void EnsureCanBorrow_ActiveNotBlocked_DoesNotThrow() // Comprueba que un usuario activo y no bloqueado pueda realizar préstamos sin errores.
    {
        var user = BuildUser();

        var ex = Record.Exception(() => user.EnsureCanBorrow());

        Assert.Null(ex);
    }

    [Fact]
    public void EnsureCanBorrow_Blocked_Throws() // Comprueba que un usuario bloqueado no pueda realizar préstamos.
    {
        var user = BuildUser();

        user.Block();

        Assert.Throws<UserBlockedException>(() =>
            user.EnsureCanBorrow());
    }

    // ── CreateClient ──────────────────────────────────────────────────────

    [Fact]
    public void CreateClient_ValidData_GeneratesLibraryCard() // Comprueba que al crear un cliente se genere automáticamente una tarjeta de biblioteca.
    {
        var user = User.CreateClient(
            ValidName,
            ValidEmail,
            ValidPhone,
            ValidRoleId,
            CreatedBy);

        Assert.NotNull(user.LibraryCardNumber);
        Assert.StartsWith("LC-", user.LibraryCardNumber!.Value);
    }

    [Fact]
    public void CreateClient_WithoutEmail_AllowsNullEmail() // Comprueba que un cliente pueda registrarse sin correo electrónico.
    {
        var user = User.CreateClient(
            ValidName,
            null,
            ValidPhone,
            ValidRoleId,
            CreatedBy);

        Assert.Null(user.Email);
    }

    [Fact]
    public void CreateClient_EmitsUserCreatedEvent() // Comprueba que al crear un cliente se genere el evento UserCreatedEvent.
    {
        var user = User.CreateClient(
            ValidName,
            ValidEmail,
            ValidPhone,
            ValidRoleId,
            CreatedBy);

        Assert.Single(user.DomainEvents.OfType<UserCreatedEvent>());
    }

    // ── ReissueLibraryCard ────────────────────────────────────────────────

    [Fact]
    public void ReissueLibraryCard_GeneratesNewCard() // Comprueba que al reemitir la tarjeta se genere un nuevo número diferente al anterior.
    {
        var user = User.CreateClient(
            ValidName,
            ValidEmail,
            ValidPhone,
            ValidRoleId,
            CreatedBy);

        var firstCard = user.LibraryCardNumber!.Value;

        user.ReissueLibraryCard();

        Assert.NotNull(user.LibraryCardNumber);

        // Cards are random — with overwhelming probability they differ
        Assert.NotEqual(firstCard, user.LibraryCardNumber!.Value);
    }

    // ── ClearDomainEvents ─────────────────────────────────────────────────

    [Fact]
    public void ClearDomainEvents_RemovesAllEvents() // Comprueba que todos los eventos de dominio pendientes se eliminen correctamente.
    {
        var user = BuildUser();

        user.Block();

        user.ClearDomainEvents();

        Assert.Empty(user.DomainEvents);
    }
}