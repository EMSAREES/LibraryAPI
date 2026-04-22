using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Events.Branches;

/// <summary>
/// Se publica cuando una sucursal es desactivada por el administrador.
/// Los handlers pueden reaccionar cancelando préstamos activos
/// o notificando a los usuarios afectados.
/// </summary>
public sealed class BranchDeactivatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Sucursal que fue desactivada.
    /// </summary>

    public Branch Branch { get; }

    public BranchDeactivatedEvent(Branch branch)
    {
        Branch = branch;
    }
}