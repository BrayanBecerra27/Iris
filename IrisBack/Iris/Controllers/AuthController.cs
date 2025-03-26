using FluentValidation;
using Iris.ViewModels.Response;
using IrisCore.DTOs;
using IrisCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAuthenticationService _iAuthenticationService;
    private readonly IValidator<UserLoginDTO> _validator;

    public AuthController(IConfiguration configuration, IAuthenticationService authenticationService, IValidator<UserLoginDTO> validator)
    {
        _configuration = configuration;
        _iAuthenticationService = authenticationService;
        _validator = validator;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
    {
        await _validator.ValidateAndThrowAsync(userLogin);
        // Here, the correct username and password should be validated to generate a token.
        // For the test, no user table is created for the test, so this data will be used.
        if (userLogin.Username == "usuario" && userLogin.Password == "1234")
        {
            return Ok(new ResultResponse<string> { Data = _iAuthenticationService.GenerateToken(userLogin) });
        }

        return Unauthorized();
    }

}
   