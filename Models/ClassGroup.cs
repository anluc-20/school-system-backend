using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models
{
    public class ClassGroup
    {
        public int Id { get; set; }
        public string? Class_Group_Name { get; set; }
        public int Topic_Id { get; set; }
        public int Instructor_Id { get; set; }
    }
}
