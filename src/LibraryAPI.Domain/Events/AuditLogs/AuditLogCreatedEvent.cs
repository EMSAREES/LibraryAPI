using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Events.AuditLogs;

public sealed class AuditLogCreatedEvent : BaseDomainEvent
{
    public LibraryAPI.Domain.Entities.AuditLogs AuditLog { get; }

    public AuditLogCreatedEvent(LibraryAPI.Domain.Entities.AuditLogs auditLog)
    {
        AuditLog = auditLog;
    }
}
