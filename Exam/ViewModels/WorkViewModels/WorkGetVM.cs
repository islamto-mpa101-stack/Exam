namespace Exam.ViewModels.WorkViewModels;

public class WorkGetVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public object CategoryName { get; internal set; }
}