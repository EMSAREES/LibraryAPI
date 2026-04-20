namespace LibraryAPI.Domain.Entities;

public class GlobalSetting
{
    public Guid Id { get; private set; }

    public string Key { get; private set; } = default!;
    public string Value { get; private set; } = default!;
    public string? Description { get; private set; }

    public DateTime UpdatedAt { get; private set; }
    public Guid UpdatedByUserId { get; private set; }
}