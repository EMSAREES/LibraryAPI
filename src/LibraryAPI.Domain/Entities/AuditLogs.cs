using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Events.AuditLogs;
using LibraryAPI.Domain.Enums;
namespace LibraryAPI.Domain.Entities;

public sealed class AuditLogs : BaseEntity
{
    public Guid UserId { get; private set; }
    public AuditAction Action { get; private set; } 
    public string EntityType { get; private set; } = string.Empty;
    public Guid EntityId { get; private set; }
    public string? OldValues { get; private set; }
    public string? NewValues { get; private set; }
    public string? IpAddress { get; private set; }
    public string? UserAgent { get; private set; }

    private AuditLogs() { }

    private AuditLogs(
        Guid userId,
        AuditAction action,
        string entityType,
        Guid entityId,
        string? oldValues,
        string? newValues,
        string? ipAddress,
        string? userAgent)
    {
        UserId = userId;
        Action = action;
        EntityType = entityType;
        EntityId = entityId;
        OldValues = oldValues;
        NewValues = newValues;
        IpAddress = ipAddress;
        UserAgent = userAgent;

        CreatedByUserId = userId;
    }

    public static AuditLogs Create(
        Guid userId,
        AuditAction action,
        string entityType,
        Guid entityId,
        string? oldValues = null,
        string? newValues = null,
        string? ipAddress = null,
        string? userAgent = null)
    {
        if (userId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(userId)
            };

        if (string.IsNullOrWhiteSpace(entityType))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(entityType)
            };

        if (entityId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(entityId)
            };

        var auditLog = new AuditLogs(userId, action, entityType, entityId, oldValues, newValues, ipAddress, userAgent);
        auditLog.AddDomainEvent(new AuditLogCreatedEvent(auditLog));
        return auditLog;
    }
}