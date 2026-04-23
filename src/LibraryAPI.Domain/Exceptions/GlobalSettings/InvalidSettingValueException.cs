using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Enums;

namespace LibraryAPI.Domain.Exceptions.GlobalSettings;

/// <summary>
/// Se lanza cuando el valor de una configuración global no es válido para su tipo o propósito.
/// Por ejemplo: un valor no numérico para una configuración que espera un número.
/// HTTP → 400 Bad Request.
/// </summary>
public sealed class InvalidSettingValueException : DomainException
{
    public GlobalSettingKey SettingKey { get; }
    public string ProvidedValue { get; }
    public string? ExpectedFormat { get; }

    public InvalidSettingValueException(GlobalSettingKey settingKey, string providedValue, string? expectedFormat = null)
        : base("INVALID_SETTING_VALUE", DomainErrors.GlobalSetting.InvalidSettingValue)
    {
        SettingKey = settingKey;
        ProvidedValue = providedValue;
        ExpectedFormat = expectedFormat;
    }
}
