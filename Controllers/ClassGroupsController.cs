using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Models.Manager;
using SchoolSystem.Models.Request;
using System.Data;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassGroupsController(IDbConnection dbConnection) : ControllerBase
    {
        private readonly ResponseFactory _responseFactory = new(dbConnection);

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query =
                @"
                    SELECT class_groups.id, class_groups.class_group_name, topics.topic_name
                    FROM class_groups
                    INNER JOIN topics ON class_groups.topic_id = topics.id
                ";
            var response = await _responseFactory.QueryAsync<ClassGroup>(query);

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = "SELECT class_group_name, topic_id, instructor_id FROM class_groups WHERE id = @id";
            var parameters = new
            {
                id
            };
            var response = await _responseFactory.QueryAsync<ClassGroup>(query, parameters);

            return Ok(response);
        }
        [HttpGet("students/{id}")]
        public async Task<IActionResult> GetStudentsByClassGroupId(int id)
        {
            var query =
                @"
                    SELECT students.id, students.last_name, students.first_name 
                    FROM students
                    INNER JOIN class_groups_students ON class_groups_students.student_id = students.id
                    WHERE class_groups_students.class_group_id = @id
                ";
            var parameters = new
            {
                id
            };
            var response = await _responseFactory.QueryAsync<Individual>(query, parameters);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClassGroupRequest request)
        {
            var query = "INSERT INTO class_groups (class_group_name, topic_id, instructor_id) VALUES (@class_group_name, @topic_id, @instructor_id)";
            var parameters = new
            {
                class_group_name = request.Class_Group_Name,
                topic_id = request.Topic_Id,
                instructor_id = request.Instructor_Id,
            };

            var response = await _responseFactory.ExecuteAsync(query, parameters);

            return Ok(response);
        }
    }
}
