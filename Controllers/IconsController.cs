using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Models.Request;
using System.Data;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IconsController(IDbConnection dbConnection) : ControllerBase
    {
        private readonly ResponseFactory _responseFactory = new(dbConnection);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = "SELECT id, icon_name FROM icons";
            var response = await _responseFactory.QueryAsync<Icon>(query);

            return Ok(response);
        }

        //SELECT rows in study_plans table, specifying a quantity and a offset
        // GET: api/studyplans/{count}/{offset}
        [HttpGet("{count}/{offset}")]
        public async Task<IActionResult> GetFew(int count, int offset)
        {
            var query = "SELECT id, icon_name FROM icons LIMIT @count OFFSET @offset";
            var parameters = new { count, offset };
            var response = await _responseFactory.QueryAsync<Icon>(query, parameters);

            return Ok(response);
        }

        /*[HttpPost]
        public async Task<IActionResult> AddIcon(StudyPlanRequest request)
        {
            var query = "INSERT INTO study_plans (year_of_creation, associated_subject) VALUES (@year_of_creation, @associated_subject);";
            var parameters = new
            {
                year_of_creation = request.Year_Of_Creation,
                associated_subject = request.Associated_Subject,
            };
            var response = await _responseFactory.ExecuteAsync(query, parameters);

            return Ok(response);
        }*/
    }
}
