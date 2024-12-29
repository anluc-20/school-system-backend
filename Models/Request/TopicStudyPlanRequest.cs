using SchoolSystem.Models.Manager;
using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models.Request
{
    public class TopicStudyPlanRequest : IEntityOperationsRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public required List<EntityOperation> Entity_Operations { get; set; }
    }
}
