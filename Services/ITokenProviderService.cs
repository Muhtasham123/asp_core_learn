namespace learn_asp_clean_structure.Services;

public interface ITokenProviderService
{
    public string GenerateToken(int Id, string Email);
}