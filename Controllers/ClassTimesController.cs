using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using System.Data;
using System.Data.Common;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassTimesController(IDbConnection dbConnection) : ControllerBase
    {
        private readonly ResponseFactory _responseFactory = new(dbConnection);


        // GET: api/<TopicsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = "SELECT id, time_start, time_end FROM class_times";
            var response = await _responseFactory.QueryAsync<ClassTime>(query);

            return Ok(response);
        }

        [HttpGet("{count}/{offset}")]
        public async Task<IActionResult> GetFew(int count, int offset)
        {
            var query = "SELECT id, time_start, time_end FROM class_times LIMIT @quantity OFFSET @offset";
            var parameters = new { quantity = count, offset = count * offset };
            var response = await _responseFactory.QueryAsync<ClassTime>(query, parameters);

            return Ok(response);
        }
    }
}
