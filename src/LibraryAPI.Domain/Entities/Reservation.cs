namespace LibraryAPI.Domain.Entities;

public class Reservation
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public Guid BranchId { get; private set; }

    public string Status { get; private set; } = default!;

    public DateTime ReservationDate { get; private set; }
    public DateTime? NotifiedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }

    public DateTime CreatedAt { get; private set; }
}