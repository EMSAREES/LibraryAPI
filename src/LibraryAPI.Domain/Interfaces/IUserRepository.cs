using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Interfaces;

/// <summary>
/// Repositorio de usuarios. Extiende el contrato genérico con
/// consultas específicas del agregado User.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Busca un usuario por su correo electrónico normalizado.
    /// Retorna null si no existe.
    /// </summary>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si ya existe un usuario registrado con ese correo.
    /// Más eficiente que GetByEmail cuando solo se necesita existencia.
    /// </summary>
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca un usuario por el número de su tarjeta de biblioteca.
    /// Se usa al registrar préstamos presenciales sin contraseña.
    /// </summary>
    Task<User?> GetByLibraryCardNumberAsync(string cardNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los usuarios asignados a una sucursal (empleados/supervisores).
    /// </summary>
    Task<IReadOnlyList<User>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los usuarios con multas sin pagar.
    /// Útil para reportes administrativos y procesos de bloqueo masivo.
    /// </summary>
    Task<IReadOnlyList<User>> GetUsersWithUnpaidFinesAsync(CancellationToken cancellationToken = default);
}