using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Reservations;

/// <summary>
/// Se lanza cuando el sistema intenta convertir una reserva en préstamo
/// pero la reserva no está en estado Notified, que es el único estado válido
/// para ser completada.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class ReservationCannotBeFulfilledException : DomainException
{
    public ReservationCannotBeFulfilledException()
        : base("RESERVATION_CANNOT_BE_FULFILLED", DomainErrors.Reservation.CannotFulfill) { }
}