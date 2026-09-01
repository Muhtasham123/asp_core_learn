namespace learn_asp_clean_structure.DTOs;

using System.ComponentModel.DataAnnotations;

public class UserLoginRequest
{
    [Required(ErrorMessage="Email is required")]
    [EmailAddress(ErrorMessage="Invalid email address")]
    public string Email {get; set;} = "";

    [Required(ErrorMessage="Password is required")]
    [MinLength(6, ErrorMessage="Password must be 6 characters long")]
    public string Password {get; set;} = "";
}