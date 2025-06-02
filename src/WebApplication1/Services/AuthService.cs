using Microsoft.AspNetCore.Identity;
using WebApplication1.Common;
using WebApplication1.Common.Contracts;
using WebApplication1.Entities;

namespace WebApplication1.Services;

public class AuthService : IAuthService
{
    private readonly ITokenClaimService _tokenClaimService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager, ITokenClaimService tokenClaimService)
    {
        _userManager = userManager;
        _tokenClaimService = tokenClaimService;
    }

    public async Task<Result<string>> RegisterAsync(string userName, string password, string name, int age,
        float weight, float height, string goal)
    {
        if (await _userManager.FindByNameAsync(userName) != null)
            return Result<string>.Failure("UserName already exists");

        var user = new ApplicationUser
        {
            UserName = userName, Email = userName, Name = name, Age = age, Weight = weight, Height = height, Goal = goal
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return Result<string>.Failure($"Failed to create user: {errors}");
        }

        await _userManager.AddToRoleAsync(user, "User");

        return await _tokenClaimService.GetTokenAsync(userName);
    }

    public async Task<Result<string>> LoginAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null) return Result<string>.Failure("UserName does not exist.");

        if (!await _userManager.CheckPasswordAsync(user, password)) return Result<string>.Failure("Invalid password.");

        return await _tokenClaimService.GetTokenAsync(user.UserName!);
    }
}