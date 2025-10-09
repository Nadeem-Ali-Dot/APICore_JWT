using System.ComponentModel.DataAnnotations;

namespace API_WithAuthorize.Models
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
    }
}
