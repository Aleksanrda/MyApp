namespace MyApp.Web.Models.Users;

public class UserViewModel
{
    public string? Forename { get; set; }

    public string? Surname { get; set; }

    public bool IsActive { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}