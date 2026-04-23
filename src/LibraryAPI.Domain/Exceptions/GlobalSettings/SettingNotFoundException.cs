using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Enums;

namespace LibraryAPI.Domain.Exceptions.GlobalSettings;

/// <summary>
/// Se lanza cuando se busca una configuración global por clave y no existe en el sistema.
/// HTTP → 404 Not Found.
/// </summary>
public sealed class SettingNotFoundException : DomainException
{
    public GlobalSettingKey SettingKey { get; }

    public SettingNotFoundException(GlobalSettingKey settingKey)
        : base("SETTING_NOT_FOUND", DomainErrors.GlobalSetting.SettingNotFound)
    {
        SettingKey = settingKey;
    }
}
