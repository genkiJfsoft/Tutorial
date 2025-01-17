using Microsoft.AspNetCore.Identity;
using ExpenseTracker.Core.Domain;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Core.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace ExpenseTracker.Core.Providers.Persistence.Services;

public class UserService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserManager<User> _userManager;
    public string Category { get; set; } = "Normal Member"; // Default value

    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _serviceProvider = serviceProvider;
    }
    public string? GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    public string GetCurrentUserIdOrDefault()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    }

    public List<User> GetAllUsers()
    {
        return _userManager.Users.ToList();
    }

    public async Task<string?> GetCurrentUserIdAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        return await Task.FromResult(userId); // Simulate async behavior
    }

    public async Task<User?> GetUserByIdAsync(string userId)
{
    return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
}

    public async Task<bool> CreateUserAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded;
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
        return false;
    }



    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        Console.WriteLine($"Total Users Retrieved: {users.Count}");
        return users;
    }
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> VerifyPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<List<User>> GetFilteredUsersAsync(string? username, string? email, DateTime? registerDateFrom, DateTime? registerDateTo)
    {
        var query = _userManager.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(username))
        {
            query = query.Where(user => user.UserName != null && user.UserName.Contains(username));
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            query = query.Where(user => user.Email != null && user.Email.Contains(email));
        }

        if (registerDateFrom.HasValue)
        {
            query = query.Where(user => user.CreatedAt >= registerDateFrom.Value);
        }

        if (registerDateTo.HasValue)
        {
            query = query.Where(user => user.CreatedAt <= registerDateTo.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<bool> UpdatePreferredLanguageAsync(string userId, string preferredLanguage)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        user.PreferredLanguage = preferredLanguage; // Ensure `PreferredLanguage` exists in the `User` model
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }


    public async Task<string?> GetPreferredLanguageAsync(string userId)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var user = await userManager.FindByIdAsync(userId);
            return user?.PreferredLanguage;
        }
    }
}
