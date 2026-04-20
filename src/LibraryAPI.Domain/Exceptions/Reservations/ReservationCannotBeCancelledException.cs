using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Reservations;

/// <summary>
/// Se lanza cuando se intenta cancelar una reserva que ya fue completada,
/// cancelada o expirada.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class ReservationCannotBeCancelledException : DomainException
{
    public ReservationCannotBeCancelledException()
        : base("RESERVATION_CANNOT_BE_CANCELLED", DomainErrors.Reservation.CannotCancel) { }
}