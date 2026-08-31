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
    public ActionResult<List<UserResponse>> GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<UserResponse> GetById(int id)
    {
        var user = _userService.GetById(id);
        if (user == null)
        {
            return NotFound(new {Message = "User does not exist"});
        }

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<UserResponse> Create(UserCreateRequest user)
    {
        
        var responseUser = _userService.Create(user);
        return CreatedAtAction(nameof(GetById), new {id = responseUser.Id}, responseUser);
    }

    [HttpPut("{id}")]
    public ActionResult<UserResponse> Update(int id, UserUpdateRequest updatedUser)
    {
        var user = _userService.Update(id, updatedUser);

        if (user == null)
        {
            return NotFound(new {Message = "User does not exist"});
        }

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var isDeleted = _userService.Delete(id);

        if (!isDeleted)
        {
            return NotFound(new { Message = "User does not exist" });
        }
        return NoContent();
    }
}