namespace LibraryAPI.Domain.Enums;

/// <summary>
/// Representa las acciones registradas en el log de auditoría del sistema.
/// </summary>
public enum AuditAction
{
    // ── Usuarios ────────────────────────────────────────────

    /// <summary>
    /// Se registró un nuevo usuario en el sistema.
    /// </summary>
    UserCreated = 1,

    /// <summary>
    /// Se actualizaron los datos de un usuario.
    /// </summary>
    UserUpdated = 2,

    /// <summary>
    /// Un usuario fue bloqueado por multas pendientes o acción administrativa.
    /// </summary>
    UserBlocked = 3,

    /// <summary>
    /// Un usuario fue desbloqueado después de saldar sus multas.
    /// </summary>
    UserUnblocked = 4,

    // ── Libros ──────────────────────────────────────────────

    /// <summary>
    /// Se registró un nuevo libro en el catálogo.
    /// </summary>
    BookCreated = 5,

    /// <summary>
    /// Se actualizó la información de un libro existente.
    /// </summary>
    BookUpdated = 6,

    /// <summary>
    /// Un libro fue desactivado del catálogo.
    /// </summary>
    BookDeactivated = 7,

    // ── Préstamos ───────────────────────────────────────────

    /// <summary>
    /// Se creó un nuevo préstamo de libro.
    /// </summary>
    LoanCreated = 8,

    /// <summary>
    /// Un libro fue devuelto correctamente dentro del plazo.
    /// </summary>
    LoanReturned = 9,

    /// <summary>
    /// Un préstamo fue marcado como vencido por superar la fecha límite.
    /// </summary>
    LoanOverdue = 10,

    /// <summary>
    /// Un préstamo fue cancelado antes de completarse.
    /// </summary>
    LoanCancelled = 11,

    // ── Multas ──────────────────────────────────────────────

    /// <summary>
    /// Se generó una nueva multa por devolución tardía.
    /// </summary>
    FineCreated = 12,

    /// <summary>
    /// Una multa fue pagada por el usuario.
    /// </summary>
    FinePaid = 13,

    // ── Reservas ────────────────────────────────────────────

    /// <summary>
    /// Se creó una nueva reserva de libro.
    /// </summary>
    ReservationCreated = 14,

    /// <summary>
    /// Se notificó al usuario que su libro reservado ya está disponible.
    /// </summary>
    ReservationNotified = 15,

    /// <summary>
    /// La reserva fue completada porque el usuario recogió el libro.
    /// </summary>
    ReservationFulfilled = 16,

    /// <summary>
    /// La reserva fue cancelada por el usuario o el sistema.
    /// </summary>
    ReservationCancelled = 17,

    /// <summary>
    /// La reserva expiró porque el usuario no recogió el libro a tiempo.
    /// </summary>
    ReservationExpired = 18,

    // ── Sucursales ──────────────────────────────────────────

    /// <summary>
    /// Se registró una nueva sucursal en el sistema.
    /// </summary>
    BranchCreated = 19,

    /// <summary>
    /// Se actualizó la información de una sucursal.
    /// </summary>
    BranchUpdated = 20,

    /// <summary>
    /// Una sucursal fue desactivada del sistema.
    /// </summary>
    BranchDeactivated = 21,

    // ── Inventario ──────────────────────────────────────────

    /// <summary>
    /// Se agregaron copias físicas de un libro a una sucursal.
    /// </summary>
    StockIncreased = 22,

    /// <summary>
    /// Se dieron de baja copias físicas de un libro en una sucursal.
    /// </summary>
    StockDecreased = 23,

    // ── Configuración ───────────────────────────────────────

    /// <summary>
    /// Se modificó una política global del sistema (multas, plazos, límites).
    /// </summary>
    GlobalSettingUpdated = 24,
}