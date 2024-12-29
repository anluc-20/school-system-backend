namespace SchoolSystem.Models.Manager
{
    public abstract class ManagerBase : IManager
    {
        protected IEntityOperationsRequest _request;

        protected abstract string AddQuery { get; }
        protected abstract string ModifyQuery { get; }

        protected abstract string DeleteQuery { get; }
        
        public ManagerBase(IEntityOperationsRequest request)
        {
            _request = request;
        }
        protected abstract object GetAddParameters(EntityOperation entityOperation);
        protected abstract object GetModifyParameters(EntityOperation entityOperation);
        protected abstract object GetDeleteParameters(EntityOperation entityOperation);
        public IEnumerable<(string query, object parameter)> Queries()
        {
            foreach (var entityOperation in _request.Entity_Operations)
            {
                (string query, object parameters) result = entityOperation.Operation switch
                {
                    Operation.ADD => (AddQuery, GetAddParameters(entityOperation)),
                    Operation.MODIFY => (ModifyQuery, GetModifyParameters(entityOperation)),
                    Operation.DELETE => (DeleteQuery, GetDeleteParameters(entityOperation)),
                    _ => throw new Exception("invalid operation"),
                };
                yield return result;
            }
        }
    }
}
