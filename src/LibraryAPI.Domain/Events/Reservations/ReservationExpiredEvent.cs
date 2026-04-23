using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Reservations;

/// <summary>
/// Se publica cuando una reserva expira porque el usuario no recogió
/// el libro dentro del plazo definido en GlobalSettings.
/// Los handlers deben liberar el libro para el siguiente en cola.
/// </summary>
public sealed class ReservationExpiredEvent : BaseDomainEvent
{
    /// <summary>
    /// Reserva que expiró.
    /// </summary>
    public Reservation Reservation { get; }

    public ReservationExpiredEvent(Reservation reservation)
    {
        Reservation = reservation;
    }
}