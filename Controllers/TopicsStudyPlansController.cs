using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models.Request;
using SchoolSystem.Models;
using System.Data;
using SchoolSystem.Models.Response;
using Dapper;
using SchoolSystem.Models.Manager;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsStudyPlansController(IDbConnection dbConnection) : ControllerBase
    {
        private readonly ResponseFactory _responseFactory = new(dbConnection);


        // GET: api/
        [HttpGet("studyPlan/{studyPlanId}")]
        public async Task<IActionResult> GetAll(int studyPlanId)
        {
            var query = "SELECT id, topic_id, study_plan_id FROM topics_study_plans WHERE study_plan_id = @study_plan_id";
            var parameters = new
            {
                study_plan_id = studyPlanId,
            };
            var response = await _responseFactory.QueryAsync<TopicStudyPlan>(query, parameters);

            return Ok(response);
        }

        [HttpGet("{count}/{offset}")]
        public async Task<IActionResult> GetFew(int count, int offset)
        {
            var query = "SELECT id, topic_id, study_plan_id FROM topics_study_plans LIMIT @quantity OFFSET @offset";
            var parameters = new { quantity = count, offset = count * offset };
            var response = await _responseFactory.QueryAsync<TopicStudyPlan>(query, parameters);

            return Ok(response);
        }

        // GET api/<TopicsController>/5
        [HttpGet("{studyPlanId}")]
        public async Task<IActionResult> GetAllById(int studyPlanId)
        {
            var query =
                @"
                    SELECT topics_study_plans.id, topics_study_plans.topic_id, topics.topic_name, topics.description
                    FROM topics_study_plans
                    INNER JOIN topics ON topics_study_plans.topic_id = topics.id
                    WHERE topics_study_plans.study_plan_id = @study_plan_id
                ";
            var parameters = new
            {
                study_plan_id = studyPlanId,
            };
            var response = await _responseFactory.QueryAsync<TopicStudyPlan>(query, parameters);

            return Ok(response);
        }

        // POST api/<TopicsController>
        [HttpPost]
        public async Task<IActionResult> Post(TopicStudyPlanRequest request)
        {
            TopicStudyPlanManager manager = new(request);

            var response = await _responseFactory.ExecuteFromManager(manager);

            return Ok(response);
        }

        // PUT api/<TopicsController>/5
        public async Task<IActionResult> Put(Topic topicRequest)
        {
            var query = "UPDATE topics SET name=@name WHERE id = @id";
            var parameters = new
            {
                id = topicRequest.Id,
                first_name = topicRequest.Topic_Name,
            };
            var response = await _responseFactory.ExecuteAsync(query, parameters);

            return Ok(response);
        }

        // DELETE api/<TopicsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = "DELETE topics WHERE id = @id";
            var response = await _responseFactory.ExecuteAsync(query, id);

            return Ok(response);
        }
    }
}
