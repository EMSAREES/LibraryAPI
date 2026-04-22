using LibraryAPI.Domain.Enums;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Exceptions.Branches;
using LibraryAPI.Domain.Events.Branches;
using LibraryAPI.Domain.ValueObjects;


public class Branch : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public Address Address { get; private set; } = null!;

    /// <summary>
    /// Teléfono de contacto de la sucursal.
    /// </summary>
    public PhoneNumber Phone { get; private set; } = null!;

    /// <summary>
    /// Correo electrónico de contacto de la sucursal.
    /// </summary>
    public Email Email { get; private set; } = null!;

    /// <summary>
    /// Indica si la sucursal está operativa.
    /// Una sucursal inactiva no acepta préstamos ni reservas.
    /// </summary>
    public bool IsActive { get; private set; }

    // Constructor privado para EF Core
    private Branch() { }

    /// <summary>
    /// Constructor privado que inicializa una sucursal con nombre, dirección, teléfono, correo y usuario creador.
    /// Se usa únicamente desde el método de fábrica <see cref="Create"/> 
    /// para garantizar que la instancia se cree con todas las validaciones de negocio.
    private Branch(
        string name,
        Address address,
        PhoneNumber phone,
        Email email,
        Guid createdByUserId)
    {
        Name = name;
        Address = address;
        Phone = phone;
        Email = email;
        IsActive = true;
        CreatedByUserId = createdByUserId;
    }

    /// <summary>
    /// Crea una nueva sucursal activa y publica el evento correspondiente.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si el nombre está vacío o el creador no es válido.
    /// </exception>
    public static Branch Create(
        string name,
        Address address,
        PhoneNumber phone,
        Email email,
        Guid createdByUserId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(Name)
            };

        if (createdByUserId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(createdByUserId)
            };

        var branch = new Branch(name, address, phone, email, createdByUserId);

        branch.AddDomainEvent(new BranchCreatedEvent(branch));

        return branch;
    }

    /// <summary>
    /// Actualiza los datos de contacto de la sucursal.
    /// </summary>
    public void Update(string name, Address address, PhoneNumber phone, Email email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(Name)
            };

        Name = name;
        Address = address;
        Phone = phone;
        Email = email;

        MarkAsUpdated();
        AddDomainEvent(new BranchUpdatedEvent(this));
    }

    /// <summary>
    /// Desactiva la sucursal. Una sucursal inactiva no puede operar préstamos.
    /// </summary>
    /// <exception cref="BranchInactiveException">
    /// Se lanza si la sucursal ya estaba inactiva.
    /// </exception>
    public void Deactivate()
    {
        if (!IsActive)
            throw new BranchInactiveException();

        IsActive = false;

        MarkAsUpdated();
        AddDomainEvent(new BranchDeactivatedEvent(this));
    }

    /// <summary>
    /// Reactiva una sucursal previamente desactivada.
    /// </summary>
    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;

        MarkAsUpdated();
    }

    /// <summary>
    /// Verifica que la sucursal esté activa antes de operar.
    /// </summary>
    /// <exception cref="BranchInactiveException">
    /// Se lanza si la sucursal está inactiva.
    /// </exception>
    public void EnsureIsActive()
    {
        if (!IsActive)
            throw new BranchInactiveException();
    }
}