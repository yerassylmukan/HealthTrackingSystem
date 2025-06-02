using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Common.Contracts;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class HealthController : ControllerBase
{
    private readonly IHealthService _healthService;
    private readonly UserManager<ApplicationUser> _userManager;

    public HealthController(IHealthService healthService, UserManager<ApplicationUser> userManager)
    {
        _healthService = healthService;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] HealthEntryRequestModel healthEntryRequestModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var result = await _healthService.SaveDailyDataAsync(user.Id, healthEntryRequestModel);
        if (result.IsFailure) return BadRequest(result.Error);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetDailyData()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var result = await _healthService.GetDailyDataAsync(user.Id);
        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDailyData()
    {
        ;
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var result = await _healthService.GetAllDailyDataAsync(user.Id);
        
        return Ok(result.Value);
    }
}