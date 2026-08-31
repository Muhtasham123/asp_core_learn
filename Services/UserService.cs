namespace learn_asp_clean_structure.Services;

using learn_asp_clean_structure.Models;
using learn_asp_clean_structure.DTOs;
using learn_asp_clean_structure.Data;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserResponse>> GetAll()
    {
        var users = await _context.Users.ToListAsync();

        return users.Select(u => MapResponse(u)).ToList();
    }

    public async Task<UserResponse?> GetById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return null;
        }

        var responseUser = MapResponse(user);
        return responseUser;
    }

    public async Task<UserResponse> Create(UserCreateRequest user)
    {
        var userModelObject = new User
        {
            Name = user.Name,
            Email = user.Email,
            PasswordHash = HashPassword(user.Password),
            Age = user.Age
        };

        _context.Users.Add(userModelObject);
        await _context.SaveChangesAsync();
        return  MapResponse(userModelObject);
    }

    public async Task<UserResponse?> Update(int id, UserUpdateRequest updatedUser)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return null;
        }

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.Age = updatedUser.Age;

        await _context.SaveChangesAsync();
        return MapResponse(user);
    }

    public async Task<bool> Delete(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
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