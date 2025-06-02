using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Common.Contracts;
using WebApplication1.Entities;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AdviceController : ControllerBase
{
    private readonly IAdviceService _adviceService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdviceController(
        IAdviceService adviceService,
        UserManager<ApplicationUser> userManager)
    {
        _adviceService = adviceService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAdvice()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var result = await _adviceService.GetAdviceForTodayAsync(user.Id);
        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }
}