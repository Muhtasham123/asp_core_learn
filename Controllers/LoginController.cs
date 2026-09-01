using learn_asp_clean_structure.DTOs;
using learn_asp_clean_structure.Services;
using Microsoft.AspNetCore.Mvc;

namespace learn_asp_clean_structure.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login(UserLoginRequest user)
    {
        var token = await _loginService.Login(user);

        if (token == null)
        {
            return NotFound(new {Message = "Invalid Credentials"});
        }

        return Ok(token);
    }
}