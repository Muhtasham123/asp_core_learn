namespace learn_asp_clean_structure.DTOs;

public class UserResponse
{
    public int Id {get; set;}
    public string Name {get; set;} = "";
    public string Email {get; set;} = "";
    public int Age {get; set;}
    public DateTime CreatedAt {get; set;}
}