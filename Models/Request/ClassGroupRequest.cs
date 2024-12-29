using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models.Request
{
    public class ClassGroupRequest
    {
        [Required]
        public string? Class_Group_Name { get; set; }
        [Required]
        public int Topic_Id { get; set; }
        [Required]
        public int Instructor_Id { get; set; }
    }
}
