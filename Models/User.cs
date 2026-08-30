namespace learn_asp_clean_structure.Models;

public class User
{
    public int Id {get; set;}
    public string Name {get; set;} = "";
    public string Email {get; set;} = "";
    public string PasswordHash {get; set;} = "";
    public int Age {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public bool IsActive {get; set;} = true;
}