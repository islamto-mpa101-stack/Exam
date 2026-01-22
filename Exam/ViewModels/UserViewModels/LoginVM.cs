using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModels.UserViewModels;

public class LoginVM
{
    [Required, EmailAddress]
    public string Email { get; set; } 
   
    [Required, MaxLength(256), MinLength(8), DataType(DataType.Password)]
    public string Password { get; set; } 
   
}


