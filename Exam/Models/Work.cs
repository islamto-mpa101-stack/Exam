using System.Diagnostics.Contracts;

namespace Exam.Models
{
    public class Work
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }

        public Category category { get; set; }
    }
}
