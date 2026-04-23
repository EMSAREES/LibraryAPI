using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.GlobalSettings;

public sealed class GlobalSettingCreatedEvent : BaseDomainEvent
{
    public GlobalSetting GlobalSetting { get; }

    public GlobalSettingCreatedEvent(GlobalSetting globalSetting)
    {
        GlobalSetting = globalSetting;
    }
}
