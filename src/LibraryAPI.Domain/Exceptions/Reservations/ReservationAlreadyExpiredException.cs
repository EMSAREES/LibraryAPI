using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Reservations;

/// <summary>
/// Se lanza cuando se intenta operar sobre una reserva que ya expiró
/// por no ser recogida dentro del plazo definido en GlobalSettings.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class ReservationAlreadyExpiredException : DomainException
{
    public ReservationAlreadyExpiredException()
        : base("RESERVATION_ALREADY_EXPIRED", DomainErrors.Reservation.AlreadyExpired) { }
}