namespace LibraryAPI.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
}