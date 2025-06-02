using Microsoft.AspNetCore.Identity;
using WebApplication1.Common;
using WebApplication1.Common.Contracts;
using WebApplication1.Entities;
using WebApplication1.Models;

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

    public async Task<Result<string>> RegisterAsync(UserRegisterModel model)
    {
        if (await _userManager.FindByNameAsync(model.Username) != null)
            return Result<string>.Failure("UserName already exists");

        var user = new ApplicationUser
        {
            UserName = model.Username, Email = model.Username, Name = model.Name, Age = model.Age,
            Weight = model.Weight, Height = model.Height, Goal = model.Goal
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return Result<string>.Failure($"Failed to create user: {errors}");
        }

        await _userManager.AddToRoleAsync(user, "User");

        return await _tokenClaimService.GetTokenAsync(model.Username);
    }

    public async Task<Result<string>> LoginAsync(UserLoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null) return Result<string>.Failure("UserName does not exist.");

        if (!await _userManager.CheckPasswordAsync(user, model.Password))
            return Result<string>.Failure("Invalid password.");

        return await _tokenClaimService.GetTokenAsync(user.UserName!);
    }
}