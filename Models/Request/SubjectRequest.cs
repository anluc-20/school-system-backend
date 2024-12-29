using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models.Request
{
    public class SubjectRequest
    {
        [Required]
        public string? Subject_Name { get; set; }
        [Required]
        public int Id_Study_Plan { get; set; }
        [Required]
        public int Id_Icon { get; set; }
    }
}
