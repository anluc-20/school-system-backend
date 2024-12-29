using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Models.Request;
using System.Data;
using Dapper;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController(IDbConnection dbConnection) : ControllerBase
    {
        private readonly ResponseFactory _responseFactory = new(dbConnection);


        // GET: api/subjects
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = "SELECT id, id_study_plan, name FROM subjects";
            var response = await _responseFactory.QueryAsync<Topic>(query);

            return Ok(response);
        }


        [HttpGet("{count}/{offset}")]
        public async Task<IActionResult> GetFew(int count, int offset)
        {
            var query = "SELECT id, id_study_plan, name FROM subjects LIMIT @count OFFSET @offset";
            var parameters = new { count, offset =count * offset };
            var response = await _responseFactory.QueryAsync<Topic>(query, parameters);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectRequest subjectRequest)
        {
            var query = "INSERT INTO subjects (subject_name, id_study_plan, id_icon) VALUES (@subject_name, @id_study_plan, @id_icon);";
            var parameters = new
            {
                subject_name = subjectRequest.Subject_Name,
                id_study_plan = subjectRequest.Id_Study_Plan,
                id_icon = subjectRequest.Id_Icon,
            };
            var response = await _responseFactory.ExecuteAsync(query, parameters);

            return Ok(response);
        }
    }
}
