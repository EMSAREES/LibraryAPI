namespace LibraryAPI.Domain.Enums;

/// <summary>
/// Claves predefinidas para las políticas globales configurables por el administrador.
/// </summary>
public enum GlobalSettingKey
{
    /// <summary>
    /// Número máximo de libros que un usuario puede tener prestados al mismo tiempo.
    /// </summary>
    MaxLoansPerUser = 1,

    /// <summary>
    /// Duración estándar en días de un préstamo desde la fecha de inicio.
    /// </summary>
    LoanDurationDays = 2,

    /// <summary>
    /// Monto cobrado por día de retraso en la devolución de un libro.
    /// </summary>
    FinePerDay = 3,

    /// <summary>
    /// Número de días que tiene el usuario para recoger un libro una vez notificado.
    /// </summary>
    ReservationExpiryDays = 4,

    /// <summary>
    /// Número de días de anticipación para enviar el recordatorio de vencimiento.
    /// </summary>
    LoanReminderDaysBeforeDue = 5,
}