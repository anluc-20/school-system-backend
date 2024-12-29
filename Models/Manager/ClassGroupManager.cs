using System.Collections;
using System.Net;

namespace SchoolSystem.Models.Manager
{
    public class ClassGroupManager(IEntityOperationsRequest request) : ManagerBase(request)
    {
        private const string ADD_QUERY =
            @"
                INSERT INTO class_groups_students (class_group_id, student_id)
                VALUES
                (@class_group_id, @student_id)
            ";
        private const string MODIFY_QUERY =
            @"
                UPDATE class_groups_students
                SET student_id = @student_id
                WHERE id = @relationship_id
            ";
        private const string DELETE_QUERY =
            @"
                DELETE FROM class_groups_students
                WHERE id = @relationship_id
            ";

        protected override string AddQuery => ADD_QUERY;
        protected override string ModifyQuery => MODIFY_QUERY;
        protected override string DeleteQuery => DELETE_QUERY;

        protected override object GetAddParameters(EntityOperation entityOperation)
        {
            return new
            {
                class_group_id = _request.Id,
                student_id = entityOperation.Replacement_Id,
            };
        }

        protected override object GetModifyParameters(EntityOperation entityOperation)
        {
            return new
            {
                student_id = entityOperation.Replacement_Id,
                relationship_id = entityOperation.Relationship_Id,
            };
        }

        protected override object GetDeleteParameters(EntityOperation entityOperation)
        {
            return new
            {
                relationship_id = entityOperation.Relationship_Id,
            };
        }
    }
}
