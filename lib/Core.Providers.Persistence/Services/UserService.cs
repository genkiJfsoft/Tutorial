using Microsoft.AspNetCore.Identity;
using ExpenseTracker.Core.Domain;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Core.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Ardalis.Result;
using Microsoft.Extensions.Localization;


namespace ExpenseTracker.Core.Providers.Persistence.Services;

public class UserService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserManager<User> _userManager;
    private readonly IStringLocalizer<ExpenseTracker.Extensions.Resources.Strings> L;
    public string Category { get; set; } = "Normal Member"; // Default value

    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider, IStringLocalizer<ExpenseTracker.Extensions.Resources.Strings> localizer)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _serviceProvider = serviceProvider;
        L = localizer;
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

    public async Task<Result> CreateUserAsync(User user, string password)
    {
        // Validate that the user object and its properties are not null
        if (user == null)
            return Result.Error("User object cannot be null.");

        if (string.IsNullOrEmpty(user.UserName))
            return Result.Error("Username cannot be null or empty.");

        if (string.IsNullOrEmpty(user.Email))
            return Result.Error("Email cannot be null or empty.");

        // Check if the username already exists for active users
        var existingUser = await _userManager.Users
            .Where(u => u.UserName == user.UserName && u.DeletedAt == null) // Only check active users
            .FirstOrDefaultAsync(u => u.UserName == user.UserName);

        if (existingUser != null)
        {
            return Result.Error(L["DuplicateUsername"]);
        }

        // Check if the email already exists for active users
        var existingEmailUser = await _userManager.Users
            .Where(u => u.Email == user.Email && u.DeletedAt == null) // Only check active users     
            .FirstOrDefaultAsync(u => u.Email == user.Email);

        if (existingEmailUser != null)
        {
            return Result.Error(L["DuplicateEmail"]);
        }

        // Attempt to create the user
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return Result.Error(L["PasswordValidation"]);
        }

        return Result.Success();
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
            user.DeletedAt = DateTime.UtcNow; // Set DeletedAt to the current timestamp
            user.UserName = $"{user.UserName}@deleted";
            user.Email = $"{user.Email}@deleted";
            user.NormalizedUserName = $"{user.UserName}@deleted";
            user.NormalizedEmail = $"{user.Email}@deleted";
            var result = await _userManager.UpdateAsync(user);

        }
        return false;
    }



    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        Console.WriteLine($"Total Users Retrieved: {users.Count}");
        return users;
    }

    public async Task<List<User>> GetActiveMembersAsync()
    {
        return await _userManager.Users
            .Where(m => m.DeletedAt == null) // Only fetch members with DeletedAt == null
            .ToListAsync();
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
        // Normalize to the start of the day
        var startOfDay = registerDateFrom.Value.Date;
        query = query.Where(user => user.CreatedAt >= startOfDay);
    }

    if (registerDateTo.HasValue)
    {
        // Normalize to the end of the day
        var endOfDay = registerDateTo.Value.Date.AddDays(1).AddTicks(-1);
        query = query.Where(user => user.CreatedAt <= endOfDay);
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
