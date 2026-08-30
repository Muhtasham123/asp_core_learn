namespace learn_asp_clean_structure.DTOs;

public class UserCreateRequest
{
    public string Name {get; set;} = "";
    public string Email {get; set;} = "";
    public string Password {get; set;} = "";
    public int Age {get; set;}
}