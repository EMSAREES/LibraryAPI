namespace LibraryAPI.Domain.Enums;

/// <summary>
/// Representa el estado de una reserva de libro.
/// </summary>
public enum ReservationStatus
{
    /// <summary>
    /// La reserva está activa y el libro aún no está disponible.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// El sistema notificó al usuario que el libro ya está disponible.
    /// </summary>
    Notified = 2,

    /// <summary>
    /// El usuario recogió el libro y la reserva se convirtió en préstamo.
    /// </summary>
    Fulfilled = 3,

    /// <summary>
    /// El usuario canceló la reserva manualmente.
    /// </summary>
    Cancelled = 4,

    /// <summary>
    /// El usuario no recogió el libro dentro del plazo de reserva y expiró.
    /// </summary>
    Expired = 5,
}