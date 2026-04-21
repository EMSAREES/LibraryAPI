using LibraryAPI.Domain.Enums;
using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Exceptions.Base;
// using LibraryAPI.Domain.Events.Branches;
using LibraryAPI.Domain.ValueObjects;

public class Branch
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string Phone { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public bool IsActive { get; private set; } = true;

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Guid CreatedByUserId { get; private set; }
}