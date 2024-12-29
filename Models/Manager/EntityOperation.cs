using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models.Manager
{
    public enum Operation
    {
        ADD, MODIFY, DELETE
    }
    public class EntityOperation
    {
        [Required]
        public int Replacement_Id { get; set; }
        [Required]
        public int Relationship_Id { get; set; }
        [Required]
        public Operation Operation { get; set; }
    }
}
