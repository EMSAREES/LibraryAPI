using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.GlobalSettings;

public sealed class GlobalSettingUpdatedEvent : BaseDomainEvent
{
    public GlobalSetting GlobalSetting { get; }

    public GlobalSettingUpdatedEvent(GlobalSetting globalSetting)
    {
        GlobalSetting = globalSetting;
    }
}
