using Dapper;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Models.Request;
using SchoolSystem.Models.Response;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualsController(IDbConnection dbConnection) : ControllerBase
    {
        private readonly ResponseFactory _responseFactory = new(dbConnection);

        // GET: api/<IndividualsController>
        [HttpGet("students")]
        public async Task<IActionResult> GetStudents()
        {
            var query = "SELECT id, first_name, last_name FROM students";
            var response = await _responseFactory.QueryAsync<Individual>(query);
           
            return Ok(response);
        }

        [HttpGet("instructors")]
        public async Task<IActionResult> GetInstructors()
        {
            var query = "SELECT id, first_name, last_name FROM instructors";
            var response = await _responseFactory.QueryAsync<Individual>(query);

            return Ok(response);
        }

        [HttpGet("{count}/{offset}")]
        public async Task<IActionResult> GetFew(int count, int offset)
        {
            var query = "SELECT id, first_name, last_name, is_student FROM students LIMIT @quantity OFFSET @offset";
            var parameters = new { quantity = count, offset = count * offset };
            var response = await _responseFactory.QueryAsync<Individual>(query, parameters);
            
            return Ok(response);
        }

        // GET api/<IndividualsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<IndividualsController>
        [HttpPost("students/")]
        public async Task<IActionResult> PostStudent(IndividualRequest individualRequest)
        {
            var query = "INSERT INTO students (id, first_name, last_name) VALUES(@id, @first_name, @last_name)";
            var parameters = new
            {
                id = individualRequest.Id,
                first_name = individualRequest.First_Name,
                last_name = individualRequest.Last_Name,
            };
            var response = await _responseFactory.ExecuteAsync(query, parameters);

            return Ok(response);
        }
        // POST api/<IndividualsController>
        [HttpPost("instructors/")]
        public async Task<IActionResult> PostInstructor(IndividualRequest individualRequest)
        {
            var query = "INSERT INTO instructors (id, first_name, last_name) VALUES(@id, @first_name, @last_name)";
            var parameters = new
            {
                id = individualRequest.Id,
                first_name = individualRequest.First_Name,
                last_name = individualRequest.Last_Name,
            };
            var response = await _responseFactory.ExecuteAsync(query, parameters);

            return Ok(response);
        }

        // PUT api/<IndividualsController>/5
        public async Task<IActionResult> Put(Individual individualRequest)
        {
            var query = "UPDATE individuals SET first_name=@first_name, last_name=@last_name WHERE id = @id";
            var parameters = new
            {
                id = individualRequest.Id,
                first_name = individualRequest.First_Name,
                last_name = individualRequest.Last_Name,
            };
            var response = await _responseFactory.ExecuteAsync(query, parameters);

            return Ok(response);
        }

        // DELETE api/<IndividualsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = "DELETE individuals WHERE id = @id";
            var response = await  _responseFactory.ExecuteAsync(query, id);
            
            return Ok(response);
        }
    }
}
