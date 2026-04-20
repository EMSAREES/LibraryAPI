using System;

namespace LibraryAPI.Domain.Entities;

/// <summary>
/// 
/// </summary>

public class User
{
    public Guid Id { get; private set; }

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string? PhoneNumber { get; private set; }

    public bool IsActive { get; private set; } = true;
    public bool IsBlocked { get; private set; } = false;

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Guid? BranchId { get; private set; }
    public Guid RoleId { get; private set; }

    public Role Role { get; private set; } = default!;
    public Branch? Branch { get; private set; }
}