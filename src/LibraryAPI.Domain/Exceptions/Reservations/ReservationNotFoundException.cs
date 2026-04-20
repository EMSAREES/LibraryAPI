using LibraryAPI.Domain.Exceptions.Base;

namespace LibraryAPI.Domain.Exceptions.Reservations;

/// <summary>
/// Se lanza cuando se busca una reserva por ID y no existe en el sistema.
/// HTTP → 404 Not Found.
/// </summary>
public sealed class ReservationNotFoundException : DomainException
{
    public Guid ReservationId { get; }

    public ReservationNotFoundException(Guid reservationId)
        : base("RESERVATION_NOT_FOUND", "La reserva solicitada no fue encontrada.")
    {
        ReservationId = reservationId;
    }
}