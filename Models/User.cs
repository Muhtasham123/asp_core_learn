namespace learn_asp_clean_structure.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}

    [Required]
    [MaxLength(100)]
    public string Name {get; set;} = "";

    [Required]
    [MaxLength(200)]
    public string Email {get; set;} = "";

    [Required]
    public string PasswordHash {get; set;} = "";

    public int Age {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public bool IsActive {get; set;} = true;
}