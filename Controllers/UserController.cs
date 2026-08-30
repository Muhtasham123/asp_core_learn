using Microsoft.AspNetCore.Mvc;
using learn_asp_clean_structure.Models;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private static List<User> _users = new List<User>();
    private static int _nextId = 0;

    [HttpGet]
    public ActionResult<List<User>> GetAll()
    {
        return Ok(_users);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetById(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(new {Message = "User does not exist"});
        }

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        user.Id = ++_nextId;
        _users.Add(user);
        return CreatedAtAction(nameof(GetById), new {id = user.Id}, user);
    }

    [HttpPut("{id}")]
    public ActionResult<User> UpdateUser(int id, User updatedUser)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound(new {Message = "User does not exist"});
        }

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.Age = updatedUser.Age;

        return Ok(user);
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
}