using Microsoft.AspNetCore.Mvc;

namespace Exam.Models
{
    public class Category 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Work> work { get; set; }
    }
}
