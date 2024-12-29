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
    public class ClassGroupsStudentsController(IDbConnection dbConnection) : ControllerBase
    {
        private readonly ResponseFactory _responseFactory = new(dbConnection);

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = "SELECT class_group_name, topic_id, instructor_id FROM class_groups WHERE id = @id";
            var parameters = new
            {
                id
            };
            var response = await _responseFactory.QueryAsync<Individual>(query, parameters);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClassGroupStudentRequest request)
        {
            ClassGroupManager manager = new(request);

            var response = await _responseFactory.ExecuteFromManager(manager);

            return Ok(response);
        }
    }
}
