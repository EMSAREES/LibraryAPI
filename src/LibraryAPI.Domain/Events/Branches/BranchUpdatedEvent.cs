using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Branches;

/// <summary>
/// Se publica cuando los datos de una sucursal son actualizados.
/// </summary>
public sealed class BranchUpdatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Sucursal con los datos actualizados.
    /// </summary>
    public Branch Branch { get; }

    public BranchUpdatedEvent(Branch branch)
    {
        Branch = branch;
    }
}