namespace learn_asp_clean_structure.Services;

using learn_asp_clean_structure.DTOs;
using learn_asp_clean_structure.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

public class LoginService : ILoginService
{
    private readonly ITokenProviderService _tokenProvider;
    private readonly AppDbContext _context;

    public LoginService(ITokenProviderService tokenProvider, AppDbContext context)
    {
        _tokenProvider = tokenProvider;
        _context = context;
    }
    public async Task<string?> Login(UserLoginRequest user)
    {
        //Verify Email existence here
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

        if (dbUser == null)
        {
            return null;
        }

        //If user exists, verify Password matches
        if (!BCrypt.Verify(user.Password, dbUser.PasswordHash))
        {
            return null;
        }

        //create token and return
        var token = _tokenProvider.GenerateToken(dbUser.Id, dbUser.Email);
        return token;      
    }
}