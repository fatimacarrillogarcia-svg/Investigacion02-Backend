using HackerRank1.DTO;
using HackerRank1.Entities;
using HackerRank1.Helpers;
using HackerRank1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackerRank1.Controllers;

public record TokenResponse(string Token);

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(IAuthenticationService authenticationService, JwtSettings jwtSettings)
    {
        _authenticationService = authenticationService;
        _jwtSettings = jwtSettings;
    }

    [HttpPost("/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var validUser = await _authenticationService.AuthenticateAsync(request.Email, request.Password);
        if (validUser is null)
            return Unauthorized();

        var token = TokenGenerator.GenerateToken(validUser, _jwtSettings);
        return Ok(new TokenResponse(token));
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
