namespace learn_asp_clean_structure.Services;

using learn_asp_clean_structure.Models;
using learn_asp_clean_structure.DTOs;

public class UserService : IUserService
{
    private static List<User> _users = new List<User>();
    private static int _nextId = 0;

    public List<UserResponse> GetAll()
    {
        var users = _users.Select(u => MapResponse(u)).ToList();
        return users;
    }

    public UserResponse? GetById(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return null;
        }

        var responseUser = MapResponse(user);
        return responseUser;
    }

    public UserResponse Create(UserCreateRequest user)
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
        return  MapResponse(userModelObject);
    }

    public UserResponse? Update(int id, UserUpdateRequest updatedUser)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return null;
        }

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.Age = updatedUser.Age;

        return MapResponse(user);
    }

    public bool Delete(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return false;
        }

        _users.Remove(user);
        return true;
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