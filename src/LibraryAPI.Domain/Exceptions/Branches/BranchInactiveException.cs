using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Branches;

/// <summary>
/// Se lanza cuando se intenta realizar una operación (préstamo, reserva, inventario)
/// en una sucursal que fue desactivada por el administrador.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class BranchInactiveException : DomainException
{
    public BranchInactiveException()
        : base("BRANCH_INACTIVE", DomainErrors.Branch.IsInactive) { }
}