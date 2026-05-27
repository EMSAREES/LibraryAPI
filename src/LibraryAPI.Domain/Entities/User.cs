using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Events.Users;
using LibraryAPI.Domain.Exceptions.Users;
using LibraryAPI.Domain.ValueObjects;


namespace LibraryAPI.Domain.Entities;

/// <summary>
/// Representa a un usuario del sistema de biblioteca.
/// Gestiona su propio ciclo de vida: registro, bloqueo, desbloqueo,
/// activación, desactivación y actualización de perfil.
/// Un usuario bloqueado no puede crear préstamos ni reservas.
/// </summary>
public sealed class User : BaseEntity
{
    public FullName FullName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = string.Empty;
    public LibraryCardNumber? LibraryCardNumber { get; private set; }
    public PhoneNumber? Phone { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsBlocked { get; private set; }
    public Guid RoleId { get; private set; }
    public Guid? BranchId { get; private set; }

    public IReadOnlyList<Loan> Loans { get; private set; } = [];
    public IReadOnlyList<Reservation> Reservations { get; private set; } = [];
    public IReadOnlyList<Fine> Fines { get; private set; } = [];

    private User() { }

    private User(
        FullName fullName,
        Email email,
        string passwordHash,
        PhoneNumber? phone,
        Guid roleId,
        Guid? branchId,
        LibraryCardNumber? libraryCardNumber,
        Guid createdByUserId)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Phone = phone;
        RoleId = roleId;
        BranchId = branchId;
        LibraryCardNumber = libraryCardNumber;
        IsActive = true;
        IsBlocked = false;
        CreatedByUserId = createdByUserId;
    }

    public static User Create(
        FullName fullName,
        Email email,
        string passwordHash,
        PhoneNumber? phone,
        Guid roleId,
        Guid? branchId,
        Guid createdByUserId)
    {
        if (fullName is null)
            throw new DomainValidationException(DomainErrors.General.RequiredFieldNull)
            { FieldName = nameof(FullName) };

        if (email is null)
            throw new DomainValidationException(DomainErrors.General.RequiredFieldNull)
            { FieldName = nameof(Email) };

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            { FieldName = nameof(PasswordHash) };

        if (roleId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            { FieldName = nameof(RoleId) };

        var user = new User(fullName, email, passwordHash, phone, roleId, branchId, libraryCardNumber: null, createdByUserId);
        user.AddDomainEvent(new UserCreatedEvent(user));
        return user;
    }

    /// <summary>
    /// Registra un nuevo cliente (sin contraseña) y le emite una tarjeta para identificarse en préstamos.
    /// </summary>
    public static User CreateClient(
        FullName fullName,
        Email email,
        PhoneNumber? phone,
        Guid roleId,
        Guid createdByUserId)
    {
        if (fullName is null)
            throw new DomainValidationException(DomainErrors.General.RequiredFieldNull)
            { FieldName = nameof(FullName) };

        if (email is null)
            throw new DomainValidationException(DomainErrors.General.RequiredFieldNull)
            { FieldName = nameof(Email) };

        if (roleId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            { FieldName = nameof(RoleId) };

        var cardNumber = ValueObjects.LibraryCardNumber.Generate();
        var user = new User(fullName, email, passwordHash: string.Empty, phone, roleId, branchId: null, libraryCardNumber: cardNumber, createdByUserId);
        user.AddDomainEvent(new UserCreatedEvent(user));
        return user;
    }

    /// <summary>
    /// Activa una cuenta de usuario previamente desactivada.
    /// </summary>
    /// <exception cref="UserAlreadyActiveException">
    /// Se lanza si el usuario ya estaba activo.
    /// </exception>
    public void Activate()
    {
        if (IsActive)
            throw new UserAlreadyActiveException();

        IsActive = true;

        MarkAsUpdated();
        AddDomainEvent(new UserUpdatedEvent(this));
    }

    /// <summary>
    /// Desactiva la cuenta de usuario. Una cuenta inactiva no puede
    /// autenticarse ni realizar préstamos o reservas.
    /// </summary>
    /// <exception cref="UserNotActiveException">
    /// Se lanza si el usuario ya estaba inactivo.
    /// </exception>
    public void Deactivate()
    {
        if (!IsActive)
            throw new UserNotActiveException();

        IsActive = false;

        MarkAsUpdated();
        AddDomainEvent(new UserUpdatedEvent(this));
    }

    /// <summary>
    /// Bloquea al usuario por multas pendientes o acción administrativa.
    /// </summary>
    public void Block()
    {
        if (IsBlocked)
            throw new UserAlreadyBlockedException();

        IsBlocked = true;

        MarkAsUpdated();
        AddDomainEvent(new UserBlockedEvent(this));
    }

    /// <summary>
    /// Desbloquea al usuario después de saldar sus multas pendientes.
    /// </summary>
    public void Unblock()
    {
        if (!IsBlocked)
            throw new UserNotBlockedException();

        IsBlocked = false;

        MarkAsUpdated();
        AddDomainEvent(new UserUnblockedEvent(this));
    }

    /// <summary>
    /// Re-emite la tarjeta de biblioteca de un cliente (por pérdida o daño).
    /// </summary>
    public void ReissueLibraryCard()
    {
        LibraryCardNumber = ValueObjects.LibraryCardNumber.Generate();
        MarkAsUpdated();
        AddDomainEvent(new UserUpdatedEvent(this));
    }

    /// <summary>
    /// Actualiza el perfil del usuario.
    /// </summary>
    public void UpdateProfile(FullName fullName, PhoneNumber? phone)
    {
        if (fullName is null)
            throw new DomainValidationException(DomainErrors.General.RequiredFieldNull)
            { FieldName = nameof(FullName) };

        FullName = fullName;
        Phone = phone;

        MarkAsUpdated();
        AddDomainEvent(new UserUpdatedEvent(this));
    }

    /// <summary>
    /// Actualiza el hash de la contraseña del usuario.
    /// </summary>
    public void UpdatePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            { FieldName = nameof(PasswordHash) };

        PasswordHash = newPasswordHash;
        MarkAsUpdated();
    }

    /// <summary>
    /// Asigna o cambia la sucursal del usuario (para empleados y supervisores).
    /// </summary>
    public void AssignBranch(Guid branchId)
    {
        if (branchId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            { FieldName = nameof(BranchId) };

        BranchId = branchId;
        MarkAsUpdated();
    }

    /// <summary>
    /// Verifica que el usuario pueda realizar préstamos o reservas.
    /// Lanza excepción si está bloqueado o inactivo.
    /// </summary>
    public void EnsureCanBorrow()
    {
        if (!IsActive)
            throw new UserNotActiveException();

        if (IsBlocked)
            throw new UserBlockedException();
    }
}