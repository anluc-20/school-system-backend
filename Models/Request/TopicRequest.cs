using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models.Request
{
    public class TopicRequest
    {
        [Required]
        public string? Topic_Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
