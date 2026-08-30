namespace learn_asp_clean_structure.DTOs;

public class UserUpdateRequest
{
    public string Name {get; set;} = "";
    public string Email {get; set;} = "";
    public int Age {get; set;}
}