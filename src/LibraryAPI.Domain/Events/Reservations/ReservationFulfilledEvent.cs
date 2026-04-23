using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Reservations;

/// <summary>
/// Se publica cuando una reserva es completada porque el usuario
/// recogió el libro y se convirtió en un préstamo activo.
/// </summary>
public sealed class ReservationFulfilledEvent : BaseDomainEvent
{
    /// <summary>
    /// Reserva que fue completada.
    /// </summary>
    public Reservation Reservation { get; }

    public ReservationFulfilledEvent(Reservation reservation)
    {
        Reservation = reservation;
    }
}