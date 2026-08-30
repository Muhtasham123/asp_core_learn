using Microsoft.AspNetCore.Mvc;
using learn_asp_clean_structure.Models;
using learn_asp_clean_structure.DTOs;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private static List<User> _users = new List<User>();
    private static int _nextId = 0;

    [HttpGet]
    public ActionResult<List<UserResponse>> GetAll()
    {
        var users = _users.Select(u => MapResponse(u));
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<UserResponse> GetById(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(new {Message = "User does not exist"});
        }

        var responseUser = MapResponse(user);
        return Ok(responseUser);
    }

    [HttpPost]
    public ActionResult<UserResponse> CreateUser(UserCreateRequest user)
    {
        var userModelObject = new User
        {
          Id = ++_nextId,
          Name = user.Name,
          Email = user.Email,
          PasswordHash = HashPassword(user.Password),
          Age = user.Age  
        };

        _users.Add(userModelObject);
        return CreatedAtAction(nameof(GetById), new {id = userModelObject.Id}, MapResponse(userModelObject));
    }

    [HttpPut("{id}")]
    public ActionResult<UserResponse> UpdateUser(int id, UserUpdateRequest updatedUser)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound(new {Message = "User does not exist"});
        }

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.Age = updatedUser.Age;

        return Ok(MapResponse(user));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound(new { Message = "User does not exist" });
        }

        _users.Remove(user);
        return NoContent();
    }

    private UserResponse MapResponse(User user)
    {
        UserResponse userResponse = new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Age = user.Age,
            CreatedAt = user.CreatedAt
        };

        return userResponse;
    }

    private string HashPassword(string password)
    {
        return Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(password)
        );
    }
}