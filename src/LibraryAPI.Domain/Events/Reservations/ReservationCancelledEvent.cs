using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Reservations;

/// <summary>
/// Se publica cuando una reserva es cancelada por el usuario
/// o por acción administrativa.
/// </summary>
public sealed class ReservationCancelledEvent : BaseDomainEvent
{
    /// <summary>
    /// Reserva que fue cancelada.
    /// </summary>
    public Reservation Reservation { get; }

    public ReservationCancelledEvent(Reservation reservation)
    {
        Reservation = reservation;
    }
}