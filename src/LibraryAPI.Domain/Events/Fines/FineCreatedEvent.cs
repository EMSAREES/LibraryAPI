using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Fines;

/// <summary>
/// Se publica cuando se genera una nueva multa por devolución tardía.
/// Los handlers deben verificar si el total de multas pendientes
/// supera el límite para bloquear al usuario.
/// </summary>
public sealed class FineCreatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Multa que fue generada.
    /// </summary>
    public Fine Fine { get; }

    public FineCreatedEvent(Fine fine)
    {
        Fine = fine;
    }
}