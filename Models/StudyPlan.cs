using MySqlConnector;
using System.Security.Cryptography.X509Certificates;

namespace SchoolSystem.Models
{
    public class StudyPlan
    {
        public int Id { get; set; }
        public int Year_Of_Creation { get; set; }
        public string? Associated_Subject { get; set; }
    }
}
