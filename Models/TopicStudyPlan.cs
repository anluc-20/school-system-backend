namespace SchoolSystem.Models
{
    public class TopicStudyPlan
    {
        public int Id { get; set; }
        public int Topic_Id { get; set; }
        public string? Topic_Name { get; set; }

        public string? Description { get; set; }
    }
}
