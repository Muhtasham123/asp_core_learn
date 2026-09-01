namespace learn_asp_clean_structure.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using learn_asp_clean_structure.DTOs;
using learn_asp_clean_structure.Services;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    private bool IsCurrentUser(int id)
    {
        var subClaimId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

        return subClaimId == id.ToString();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<UserResponse>> GetById(int id)
    {
        if (!IsCurrentUser(id))
        {
            return Forbid();
        }

        var user = await _userService.GetById(id);
        if (user == null)
        {
            return NotFound(new {Message = "User does not exist"});
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> Create(UserCreateRequest user)
    {
        
        var responseUser = await _userService.Create(user);
        return CreatedAtAction(nameof(GetById), new {id = responseUser.Id}, responseUser);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<UserResponse>> Update(int id, UserUpdateRequest updatedUser)
    {
        if (!IsCurrentUser(id))
        {
            return Forbid();
        }

        var user = await _userService.Update(id, updatedUser);

        if (user == null)
        {
            return NotFound(new {Message = "User does not exist"});
        }

        return Ok(user);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
        if (!IsCurrentUser(id))
        {
            return Forbid();
        }

        var isDeleted = await _userService.Delete(id);

        if (!isDeleted)
        {
            return NotFound(new { Message = "User does not exist" });
        }
        return NoContent();
    }
}