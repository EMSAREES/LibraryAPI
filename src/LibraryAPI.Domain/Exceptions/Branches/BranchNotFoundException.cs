using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Branches;

/// <summary>
/// Se lanza cuando se busca una sucursal por ID y no existe en el sistema.
/// HTTP → 404 Not Found.
/// </summary>
public sealed class BranchNotFoundException : DomainException
{
    public Guid BranchId { get; }

    public BranchNotFoundException(Guid branchId)
        : base("BRANCH_NOT_FOUND", DomainErrors.Branch.NotFound)
    {
        BranchId = branchId;
    }
}