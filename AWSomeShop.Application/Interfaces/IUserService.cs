using AWSomeShop.Application.DTOs;

namespace AWSomeShop.Application.Interfaces;

public interface IUserService
{
    Task<List<UserListItemDto>> GetUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto> CreateUserAsync(CreateUserRequest request);
    Task<UserDto?> UpdateUserAsync(int id, UpdateUserRequest request);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null);
}