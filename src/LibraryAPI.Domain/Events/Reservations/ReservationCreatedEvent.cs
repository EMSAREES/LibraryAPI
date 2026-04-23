using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Reservations;

/// <summary>
/// Se publica cuando un usuario crea una reserva para un libro no disponible.
/// </summary>
public sealed class ReservationCreatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Reserva que fue creada.
    /// </summary>
    public Reservation Reservation { get; }

    public ReservationCreatedEvent(Reservation reservation)
    {
        Reservation = reservation;
    }
}