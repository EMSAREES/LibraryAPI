using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Reservations;

/// <summary>
/// Se publica cuando el sistema notifica al usuario que su libro reservado
/// ya está disponible para recoger en la sucursal.
/// A partir de este momento empieza a correr el plazo de expiración.
/// </summary>
public sealed class ReservationNotifiedEvent : BaseDomainEvent
{
    /// <summary>
    /// Reserva que fue notificada al usuario.
    /// </summary>
    public Reservation Reservation { get; }

    public ReservationNotifiedEvent(Reservation reservation)
    {
        Reservation = reservation;
    }
}