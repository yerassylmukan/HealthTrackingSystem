using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Common.Contracts;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Register([FromBody] UserRegisterModel userModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(userModel.Username, userModel.Password, userModel.Name,
            userModel.Age, userModel.Weight, userModel.Height, userModel.Goal);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }

        [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] UserLoginModel userModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authService.LoginAsync(userModel.Username, userModel.Password);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{token}")]
    public IActionResult GetPayload(string token)
    {
        if (string.IsNullOrEmpty(token)) return BadRequest("Token is required.");

        try
        {
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token)) return BadRequest("The token is not in a valid JWT format.");

            var jwtToken = handler.ReadJwtToken(token);

            var payload = jwtToken.Claims
                .ToDictionary(claim => claim.Type, claim => (object)claim.Value);

            return Ok(payload);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}