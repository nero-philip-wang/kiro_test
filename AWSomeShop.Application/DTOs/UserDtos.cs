using System.ComponentModel.DataAnnotations;

namespace AWSomeShop.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Role { get; set; }
    public bool IsActive { get; set; }
    public string Language { get; set; } = "zh-CN";
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

public class CreateUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public string Name { get; set; } = string.Empty;

    public int Role { get; set; } = 0; // Default to Employee
}

public class UpdateUserRequest
{
    [Required]
    [MinLength(1)]
    public string Name { get; set; } = string.Empty;

    public int? Role { get; set; }
    public bool? IsActive { get; set; }
    public string? Language { get; set; }
}

public class UserListItemDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Language { get; set; } = "zh-CN";
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}