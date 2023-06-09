using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Models.Users;

public class UserViewModel
{
    public int Id { get; set; }
    
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Forename can only contain letters")]
    [Required(ErrorMessage = "Forename is required")]
    
    public string? Forename { get; set; }

    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Surname can only contain letters")]
    [Required(ErrorMessage = "Surname is required")]
    public string? Surname { get; set; }

    public bool IsActive { get; set; }
    
    [Range(typeof(DateTime), "1/1/1923", "1/1/2024", ErrorMessage = "Date of Birth cannot be in the future")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateOfBirth { get; set; }
}