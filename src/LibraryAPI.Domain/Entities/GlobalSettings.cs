using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Enums;
using LibraryAPI.Domain.Events.GlobalSettings;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Exceptions.GlobalSettings;

namespace LibraryAPI.Domain.Entities;

public sealed class GlobalSetting : BaseEntity
{
    public GlobalSettingKey Key { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid UpdatedByUserId { get; private set; }

    private GlobalSetting() { }

    private GlobalSetting(GlobalSettingKey key, string value, string? description, Guid updatedByUserId)
    {
        Key = key;
        Value = value;
        Description = description;
        UpdatedByUserId = updatedByUserId;

        CreatedByUserId = updatedByUserId;
    }

    public static GlobalSetting Create(GlobalSettingKey key, string value, string? description, Guid createdByUserId)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidSettingValueException(key, value);

        if (createdByUserId == Guid.Empty)
            throw new InvalidSettingValueException(key, value);

        var setting = new GlobalSetting(key, value, description, createdByUserId);
        setting.AddDomainEvent(new GlobalSettingCreatedEvent(setting));
        return setting;
    }

    public void Update(string value, string? description, Guid updatedByUserId)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidSettingValueException(Key, value);

        if (updatedByUserId == Guid.Empty)
            throw new InvalidSettingValueException(Key, value);

        Value = value;
        Description = description;
        UpdatedByUserId = updatedByUserId;

        MarkAsUpdated();
        AddDomainEvent(new GlobalSettingUpdatedEvent(this));
    }
}