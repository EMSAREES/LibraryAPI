namespace LibraryAPI.Domain.Enums;

/// <summary>
/// Tipo de notificación enviada al usuario por el sistema.
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// Notificación de que el libro reservado ya está disponible para recoger.
    /// </summary>
    ReservationAvailable = 1,

    /// <summary>
    /// Recordatorio de que la fecha límite de devolución está próxima.
    /// </summary>
    LoanDueSoon = 2,

    /// <summary>
    /// Aviso de que el préstamo ya venció y se generará una multa.
    /// </summary>
    LoanOverdue = 3,

    /// <summary>
    /// Confirmación de que la multa fue registrada en el sistema.
    /// </summary>
    FineGenerated = 4,

    /// <summary>
    /// Confirmación de pago de multa y desbloqueo de la cuenta.
    /// </summary>
    FinePaid = 5,

    /// <summary>
    /// Aviso de que la reserva expiró por no ser recogida a tiempo.
    /// </summary>
    ReservationExpired = 6,
}