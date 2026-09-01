namespace learn_asp_clean_structure.Services;

using learn_asp_clean_structure.DTOs;

public interface ILoginService{
    public Task<string?> Login(UserLoginRequest user);
}