using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AWSomeShop.Application.DTOs;
using AWSomeShop.Application.Interfaces;
using AWSomeShop.Domain.Entities;
using System.Security.Claims;

namespace AWSomeShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get all users (Admin+)
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<List<UserListItemDto>>> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    /// <summary>
    /// Get user by ID (Admin+)
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound(new { error = new { code = "NOT_FOUND", message = "用户不存在" } });
        }
        return Ok(user);
    }

    /// <summary>
    /// Create a new user (Admin+)
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserRequest request)
    {
        // Check email uniqueness
        if (!await _userService.IsEmailUniqueAsync(request.Email))
        {
            return BadRequest(new { error = new { code = "EMAIL_EXISTS", message = "邮箱已被注册" } });
        }

        // Validate role permissions
        var currentUserRole = GetCurrentUserRole();
        var requestedRole = (UserRole)request.Role;

        // Only SuperAdmin can create Admin or SuperAdmin
        if (requestedRole != UserRole.Employee && currentUserRole != UserRole.SuperAdmin)
        {
            return Forbid();
        }

        var user = await _userService.CreateUserAsync(request);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    /// <summary>
    /// Update a user (Admin+)
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserRequest request)
    {
        // Check email uniqueness if email is being changed (not in this request, but for future)
        // Get the existing user to check role changes
        var existingUser = await _userService.GetUserByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound(new { error = new { code = "NOT_FOUND", message = "用户不存在" } });
        }

        // Validate role permissions
        var currentUserRole = GetCurrentUserRole();
        
        // If trying to promote to Admin or SuperAdmin, only SuperAdmin can do it
        if (request.Role.HasValue && request.Role.Value != (int)UserRole.Employee)
        {
            if (currentUserRole != UserRole.SuperAdmin)
            {
                return Forbid();
            }
        }

        // If current user is not SuperAdmin, they cannot modify SuperAdmin
        if (existingUser.Role == (int)UserRole.SuperAdmin && currentUserRole != UserRole.SuperAdmin)
        {
            return Forbid();
        }

        var user = await _userService.UpdateUserAsync(id, request);
        if (user == null)
        {
            return NotFound(new { error = new { code = "NOT_FOUND", message = "用户不存在" } });
        }
        return Ok(user);
    }

    /// <summary>
    /// Delete a user (SuperAdmin only)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Policy = "SuperAdmin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        // Get the existing user to check if it's a SuperAdmin
        var existingUser = await _userService.GetUserByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound(new { error = new { code = "NOT_FOUND", message = "用户不存在" } });
        }

        // Prevent deletion of SuperAdmin by non-SuperAdmin
        if (existingUser.Role == (int)UserRole.SuperAdmin && GetCurrentUserRole() != UserRole.SuperAdmin)
        {
            return Forbid();
        }

        var result = await _userService.DeleteUserAsync(id);
        if (!result)
        {
            return NotFound(new { error = new { code = "NOT_FOUND", message = "用户不存在" } });
        }
        return NoContent();
    }

    /// <summary>
    /// Get current user info
    /// </summary>
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var userId = GetCurrentUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var user = await _userService.GetUserByIdAsync(userId.Value);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    /// <summary>
    /// Update current user profile
    /// </summary>
    [HttpPut("me")]
    public async Task<ActionResult<UserDto>> UpdateCurrentUser([FromBody] UpdateUserRequest request)
    {
        var userId = GetCurrentUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        // Employees cannot change role
        var updateRequest = new UpdateUserRequest
        {
            Name = request.Name,
            IsActive = null, // Cannot change active status
            Role = null, // Cannot change role
            Language = request.Language
        };

        var user = await _userService.UpdateUserAsync(userId.Value, updateRequest);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    private int? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(userIdClaim, out var userId) ? userId : null;
    }

    private UserRole GetCurrentUserRole()
    {
        var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
        return int.TryParse(roleClaim, out var role) ? (UserRole)role : UserRole.Employee;
    }
}