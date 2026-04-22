using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Branches;

/// <summary>
/// Se publica cuando una nueva sucursal es registrada en el sistema.
/// </summary>
public sealed class BranchCreatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Sucursal que fue creada.
    /// </summary>
    public Branch Branch { get; }

    public BranchCreatedEvent(Branch branch)
    {
        Branch = branch;
    }
}