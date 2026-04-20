namespace LibraryAPI.Domain.Entities;

public class Role
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
}