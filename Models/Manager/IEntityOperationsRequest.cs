using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models.Manager
{
    public interface IEntityOperationsRequest
    {
        public int Id { get; set; }
        public List<EntityOperation> Entity_Operations { get; set; }
    }
}
