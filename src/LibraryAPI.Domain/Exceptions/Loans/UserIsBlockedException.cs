using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Loans;

/// <summary>
/// Se lanza cuando un usuario bloqueado intenta crear un préstamo.
/// HTTP → 403 Forbidden.
/// NOTA: Diferente de UserBlockedException (en Users/) — esta pertenece
/// al contexto de Loans para señalar explícitamente que el bloqueo
/// impide la operación de préstamo.
/// </summary>
public sealed class UserIsBlockedException : DomainException
{
    public UserIsBlockedException()
        : base("USER_IS_BLOCKED", DomainErrors.Loan.UserIsBlocked) { }
}