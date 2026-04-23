using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Enums;

namespace LibraryAPI.Domain.Exceptions.GlobalSettings;

/// <summary>
/// Se lanza cuando se intenta crear una configuración global con una clave que ya existe.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class SettingAlreadyExistsException : DomainException
{
    public GlobalSettingKey SettingKey { get; }

    public SettingAlreadyExistsException(GlobalSettingKey settingKey)
        : base("SETTING_ALREADY_EXISTS", DomainErrors.GlobalSetting.SettingAlreadyExists)
    {
        SettingKey = settingKey;
    }
}
