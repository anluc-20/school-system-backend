namespace SchoolSystem.Models.Manager
{
    public interface IManager
    {
        public IEnumerable<(string query, object parameter)> Queries();
    }
}
