using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModels.WorkViewModels;
public class WorkUpdateVM
{
    public int Id { get; set; }
    [Required, MaxLength(256), MinLength(3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int CategoryId { get; set; }
    public IFormFile? Image { get; set; }
}
