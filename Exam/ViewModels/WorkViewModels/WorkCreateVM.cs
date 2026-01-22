using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModels.WorkViewModels;

public class WorkCreateVM
{
    [Required, MaxLength(256), MinLength(3)]
    public string Name { get; set; } = string.Empty;
        
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public IFormFile Image { get; set; } = null!;
}