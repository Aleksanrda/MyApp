using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Models.Users;

public class CreateUserViewModel
{
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Forename can only contain letters")]
    [Required(ErrorMessage = "Forename is required")]
    public string? Forename { get; set; }

    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Surname can only contain letters")]
    [Required(ErrorMessage = "Surname is required")]
    public string? Surname { get; set; }
    
    public UserStatus Status { get; set; }
    
    [Range(typeof(DateTime), "1/1/1923", "1/1/2024", ErrorMessage = "Date of Birth cannot be in the future")]
    public DateTime DateOfBirth { get; set; }
}