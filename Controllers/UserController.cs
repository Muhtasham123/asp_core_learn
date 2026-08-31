namespace learn_asp_clean_structure.Controllers;

using Microsoft.AspNetCore.Mvc;
using learn_asp_clean_structure.DTOs;
using learn_asp_clean_structure.Services;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetById(int id)
    {
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
    public async Task<ActionResult<UserResponse>> Update(int id, UserUpdateRequest updatedUser)
    {
        var user = await _userService.Update(id, updatedUser);

        if (user == null)
        {
            return NotFound(new {Message = "User does not exist"});
        }

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var isDeleted = await _userService.Delete(id);

        if (!isDeleted)
        {
            return NotFound(new { Message = "User does not exist" });
        }
        return NoContent();
    }
}