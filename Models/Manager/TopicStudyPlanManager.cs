using SchoolSystem.Models.Request;
using System.Collections;
using System.Net;

namespace SchoolSystem.Models.Manager
{
    public class TopicStudyPlanManager(IEntityOperationsRequest request) : ManagerBase(request)
    {
        private const string MODIFY_QUERY =
            @"
                UPDATE topics_study_plans
                SET topic_id = @topic_id
                WHERE id = @relationship_id
            ";

        private const string DELETE_QUERY =
            @"
                DELETE FROM topics_study_plans
                WHERE id = @relationship_id
            ";
        private const string ADD_QUERY =
            @"
                INSERT INTO topics_study_plans (topic_id, study_plan_id)
                VALUES
                (@topic_id, @study_plan_id)
            ";

        protected override string AddQuery => ADD_QUERY;
        protected override string ModifyQuery => MODIFY_QUERY;
        protected override string DeleteQuery => DELETE_QUERY;

        protected override object GetAddParameters(EntityOperation entityOperation)
        {
            return new
            {
                topic_id = entityOperation.Replacement_Id,
                study_plan_id = _request.Id,
            };
        }

        protected override object GetModifyParameters(EntityOperation entityOperation)
        {
            return new
            {
                topic_id = entityOperation.Replacement_Id,
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
