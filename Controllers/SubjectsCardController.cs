using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using System.Data;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsCardController(IDbConnection dbConnection) : ControllerBase
    {
        private readonly ResponseFactory _responseFactory = new(dbConnection);

        // GET: api/subjectscard
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = @"SELECT subjects.subject_name, icons.icon_name FROM subjects
                        INNER JOIN icons
                        ON subjects.id_icon = icons.id;";
            var response = await _responseFactory.QueryAsync<SubjectCard>(query);

            return Ok(response);
        }

    }
}
