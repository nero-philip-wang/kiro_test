using AWSomeShop.Application.DTOs;
using AWSomeShop.Application.Interfaces;
using AWSomeShop.Domain.Entities;
using AWSomeShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AWSomeShop.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserListItemDto>> GetUsersAsync()
    {
        var users = await _context.Users
            .OrderBy(u => u.CreatedAt)
            .ToListAsync();

        return users.Select(u => MapToListItemDto(u)).ToList();
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user == null ? null : MapToDto(user);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
    {
        // Generate a random password
        var password = GenerateRandomPassword();
        
        var user = new User
        {
            Email = request.Email.ToLower().Trim(),
            Name = request.Name.Trim(),
            Role = (UserRole)(request.Role == 0 ? 0 : request.Role),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            IsActive = true,
            Language = "zh-CN",
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // TODO: Send email with password (requirement 2.3)
        // For now, we'll just log it
        Console.WriteLine($"[Email] Initial password for {user.Email}: {password}");

        return MapToDto(user);
    }

    public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserRequest request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        user.Name = request.Name.Trim();
        
        if (request.Role.HasValue)
        {
            user.Role = (UserRole)request.Role.Value;
        }
        
        if (request.IsActive.HasValue)
        {
            user.IsActive = request.IsActive.Value;
        }
        
        if (!string.IsNullOrEmpty(request.Language))
        {
            user.Language = request.Language;
        }

        await _context.SaveChangesAsync();

        return MapToDto(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null)
    {
        var normalizedEmail = email.ToLower().Trim();
        
        var query = _context.Users.Where(u => u.Email == normalizedEmail);
        
        if (excludeUserId.HasValue)
        {
            query = query.Where(u => u.Id != excludeUserId.Value);
        }

        return !await query.AnyAsync();
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Role = (int)user.Role,
            IsActive = user.IsActive,
            Language = user.Language,
            CreatedAt = user.CreatedAt,
            LastLoginAt = user.LastLoginAt
        };
    }

    private static UserListItemDto MapToListItemDto(User user)
    {
        return new UserListItemDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            RoleName = GetRoleName(user.Role),
            IsActive = user.IsActive,
            Language = user.Language,
            CreatedAt = user.CreatedAt,
            LastLoginAt = user.LastLoginAt
        };
    }

    private static string GetRoleName(UserRole role)
    {
        return role switch
        {
            UserRole.Employee => "员工",
            UserRole.Admin => "普通管理员",
            UserRole.SuperAdmin => "超级管理员",
            _ => "未知"
        };
    }

    private static string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz23456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}