using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Fines;

/// <summary>
/// Se lanza cuando se busca una multa por ID y no existe en el sistema.
/// HTTP → 404 Not Found.
/// </summary>
public sealed class FineNotFoundException : DomainException
{
    public Guid FineId { get; }

    public FineNotFoundException(Guid fineId)
        : base("FINE_NOT_FOUND", DomainErrors.Fine.FineNotFound)
    {
        FineId = fineId;
    }
}