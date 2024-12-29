using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models.Request
{
    public class StudyPlanRequest
    {
        [Required]
        public int Year_Of_Creation { get; set; }
        [Required]
        public string? Associated_Subject { get; set; }
    }
}
