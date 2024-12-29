using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using System.Data;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyPlansCardController(IDbConnection dbConnection) : ControllerBase
    {
        private readonly ResponseFactory _responseFactory = new(dbConnection);

        // GET: api/studyplanscard
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = "SELECT year_of_creation, associated_subject FROM study_plans";
            var response = await _responseFactory.QueryAsync<StudyPlanCard>(query);

            return Ok(response);
        }

        //SELECT rows in study_plans table, specifying a quantity and a offset
        // GET: api/studyplanscard/{count}/{offset}
        [HttpGet("{count}/{offset}")]
        public async Task<IActionResult> GetFew(int count, int offset)
        {
            var query = "SELECT year_of_creation, associated_subject FROM study_plans LIMIT @count OFFSET @offset";
            var parameters = new { count, offset };
            var response = await _responseFactory.QueryAsync<StudyPlanCard>(query, parameters);

            return Ok(response);
        }

    }
}
