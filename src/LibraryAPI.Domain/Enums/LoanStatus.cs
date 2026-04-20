namespace LibraryAPI.Domain.Enums;

/// <summary>
/// Representa el estado actual de un préstamo de libro.
/// </summary>
public enum LoanStatus
{
    /// <summary>
    /// El préstamo está activo y el libro aún no ha sido devuelto.
    /// </summary>
    Active = 1,

    /// <summary>
    /// El libro fue devuelto dentro del plazo establecido.
    /// </summary>
    Returned = 2,

    /// <summary>
    /// El plazo de devolución venció y el libro no ha sido devuelto.
    /// </summary>
    Overdue = 3,

    /// <summary>
    /// El préstamo fue cancelado antes de que el usuario recogiera el libro.
    /// </summary>
    Cancelled = 4,
}