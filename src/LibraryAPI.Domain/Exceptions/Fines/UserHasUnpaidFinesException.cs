using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Fines;

/// <summary>
/// Se lanza cuando un usuario con multas sin pagar intenta crear
/// un nuevo préstamo o reserva. Bloquea la operación hasta saldar la deuda.
/// HTTP → 403 Forbidden.
/// </summary>
public sealed class UserHasUnpaidFinesException : DomainException
{
    /// <summary>
    /// Monto total de multas pendientes del usuario.
    /// </summary>
    public decimal TotalUnpaid { get; }

    public UserHasUnpaidFinesException(decimal totalUnpaid)
        : base("USER_HAS_UNPAID_FINES", DomainErrors.Fine.UserHasUnpaidFines)
    {
        TotalUnpaid = totalUnpaid;
    }
}