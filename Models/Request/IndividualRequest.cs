using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models.Request
{
    public class IndividualRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? First_Name { get; set; }
        [Required]
        public string? Last_Name { get; set; }
    }
}
