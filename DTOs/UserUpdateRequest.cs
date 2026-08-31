namespace learn_asp_clean_structure.DTOs;

using System.ComponentModel.DataAnnotations;
public class UserUpdateRequest
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(8, ErrorMessage = "Name must be at least 8 characters")]
    [MaxLength(100, ErrorMessage = "Name Must be less than 100 characters")]
    public string Name {get; set;} = "";

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email {get; set;} = "";

    [Range(1, 150, ErrorMessage = "Age must be between 1 and 150")]
    public int Age {get; set;}
}