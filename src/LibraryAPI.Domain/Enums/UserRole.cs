namespace LibraryAPI.Domain.Enums;

/// <summary>
/// Define los roles disponibles dentro del sistema de biblioteca.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Acceso total al sistema: sucursales, empleados y políticas globales.
    /// </summary>
    Admin = 1,

    /// <summary>
    /// Puede gestionar el catálogo de libros y consultar información de empleados.
    /// </summary>
    Supervisor = 2,

    /// <summary>
    /// Puede consultar el catálogo y registrar préstamos a usuarios.
    /// </summary>
    Employee = 3,

    /// <summary>
    /// Puede consultar el catálogo y gestionar sus propios préstamos y reservas.
    /// </summary>
    Client = 4,
}