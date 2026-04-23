using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.GlobalSettings;

/// <summary>
/// Se lanza cuando se intenta usar una clave de configuración inválida o no reconocida.
/// HTTP → 400 Bad Request.
/// </summary>
public sealed class InvalidSettingKeyException : DomainException
{
    public string ProvidedKey { get; }

    public InvalidSettingKeyException(string providedKey)
        : base("INVALID_SETTING_KEY", DomainErrors.GlobalSetting.InvalidSettingKey)
    {
        ProvidedKey = providedKey;
    }
}
