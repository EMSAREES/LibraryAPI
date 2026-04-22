using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Enums;
using LibraryAPI.Domain.Events.Users;
using LibraryAPI.Domain.Exceptions.Users;
using LibraryAPI.Domain.ValueObjects;

namespace LibraryAPI.Domain.Entities;

/// <summary>
/// Representa a un usuario del sistema de biblioteca.
/// Gestiona su propio ciclo de vida: registro, bloqueo, desbloqueo
/// y actualización de perfil.
/// Un usuario bloqueado no puede crear préstamos ni reservas.
/// </summary>
public sealed class User : BaseEntity
{
    /// <summary>
    /// Nombre completo del usuario.
    /// </summary>
    public FullName FullName { get; private set; } = null!;

    /// <summary>
    /// Correo electrónico único del usuario. Se usa como login.
    /// </summary>
    public Email Email { get; private set; } = null!;

    /// <summary>
    /// Hash de la contraseña. Nunca se almacena en texto plano.
    /// </summary>
    public string PasswordHash { get; private set; } = string.Empty;

    /// <summary>
    /// Teléfono de contacto del usuario.
    /// </summary>
    public PhoneNumber? Phone { get; private set; }

    /// <summary>
    /// Indica si la cuenta está activa en el sistema.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Indica si el usuario está bloqueado por multas pendientes.
    /// Un usuario bloqueado no puede solicitar préstamos ni reservas.
    /// </summary>
    public bool IsBlocked { get; private set; }

    /// <summary>
    /// Identificador del rol asignado al usuario.
    /// </summary>
    public Guid RoleId { get; private set; }

    /// <summary>
    /// Identificador de la sucursal a la que está asignado (solo empleados).
    /// Nulo para clientes.
    /// </summary>
    public Guid? BranchId { get; private set; }

    /// <summary>
    /// Préstamos realizados por este usuario.
    /// </summary>
    public IReadOnlyList<Loan> Loans { get; private set; } = [];

    /// <summary>
    /// Reservas activas del usuario.
    /// </summary>
    public IReadOnlyList<Reservation> Reservations { get; private set; } = [];

    /// <summary>
    /// Multas pendientes o historial de multas del usuario.
    /// </summary>
    public IReadOnlyList<Fine> Fines { get; private set; } = [];

    // Constructor privado para EF Core
    private User() { }

    private User(
        FullName fullName,
        Email email,
        string passwordHash,
        PhoneNumber? phone,
        Guid roleId,
        Guid? branchId,
        Guid createdByUserId)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Phone = phone;
        RoleId = roleId;
        BranchId = branchId;
        IsActive = true;
        IsBlocked = false;
        CreatedByUserId = createdByUserId;
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema y publica el evento de creación.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si algún campo obligatorio es nulo o inválido.
    /// </exception>
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
            {
                FieldName = nameof(FullName)
            };

        if (email is null)
            throw new DomainValidationException(DomainErrors.General.RequiredFieldNull)
            {
                FieldName = nameof(Email)
            };

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(PasswordHash)
            };

        if (roleId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(RoleId)
            };

        var user = new User(fullName, email, passwordHash, phone, roleId, branchId, createdByUserId);

        user.AddDomainEvent(new UserCreatedEvent(user));

        return user;
    }

    /// <summary>
    /// Bloquea al usuario por multas pendientes o acción administrativa.
    /// </summary>
    /// <exception cref="UserAlreadyBlockedException">
    /// Se lanza si el usuario ya estaba bloqueado.
    /// </exception>
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
    /// <exception cref="DomainValidationException">
    /// Se lanza si el usuario no estaba bloqueado.
    /// </exception>
    public void Unblock()
    {
        if (!IsBlocked)
            throw new DomainValidationException(DomainErrors.User.NotBlocked);

        IsBlocked = false;

        MarkAsUpdated();
        AddDomainEvent(new UserUnblockedEvent(this));
    }

    /// <summary>
    /// Actualiza el perfil del usuario.
    /// </summary>
    public void UpdateProfile(FullName fullName, PhoneNumber? phone)
    {
        if (fullName is null)
            throw new DomainValidationException(DomainErrors.General.RequiredFieldNull)
            {
                FieldName = nameof(FullName)
            };

        FullName = fullName;
        Phone = phone;

        MarkAsUpdated();
        AddDomainEvent(new UserUpdatedEvent(this));
    }

    /// <summary>
    /// Actualiza el hash de la contraseña del usuario.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si el nuevo hash está vacío.
    /// </exception>
    public void UpdatePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(PasswordHash)
            };

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
            {
                FieldName = nameof(BranchId)
            };

        BranchId = branchId;

        MarkAsUpdated();
    }

    /// <summary>
    /// Verifica que el usuario pueda realizar préstamos o reservas.
    /// Lanza excepción si está bloqueado o inactivo.
    /// </summary>
    /// <exception cref="UserBlockedException">
    /// Se lanza si el usuario está bloqueado.
    /// </exception>
    public void EnsureCanBorrow()
    {
        if (IsBlocked)
            throw new UserBlockedException();

        if (!IsActive)
            throw new DomainValidationException(DomainErrors.User.NotFound);
    }
}