using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Loans;

/// <summary>
/// Se lanza cuando el usuario intenta crear un préstamo pero ya alcanzó
/// el límite máximo de préstamos simultáneos definido en GlobalSettings.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class MaxLoansExceededException : DomainException
{
    public int MaxAllowed { get; }

    public MaxLoansExceededException(int maxAllowed)
        : base("MAX_LOANS_EXCEEDED", DomainErrors.Loan.MaxLoansReached)
    {
        MaxAllowed = maxAllowed;
    }
}