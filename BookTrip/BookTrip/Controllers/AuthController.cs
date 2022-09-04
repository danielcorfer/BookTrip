using BookTrip.Interfaces;
using BookTrip.Models.UserModel.DTOs;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    public ITokenService tokenService;
    public IUserService userService;

    public AuthController(IUserService userService, ITokenService tokenService)
    {
        this.userService = userService;
        this.tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(UserRegisterDTO request)
    {
        var newUser = userService.GetUserByEmail(request.Email);
        if (newUser == null)
        {
            return Ok(userService.CreateNewUser(request));
        }
        return BadRequest(userService.CreateNewUser(request));
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserLoginDTO request)
    {
        var actualUser = userService.GetUserByEmail(request.Email);
        if (actualUser == null || !actualUser.PasswordCheck(request.Password))
        {
            return BadRequest("User not found or the password you entered is incorrect");
        }

        string token = tokenService.CreateLoginToken(actualUser);

        return Ok(token);
    }
}

