using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using SchoolSystem.Models;
using System.Data;
using Dapper;
using SchoolSystem.Models.Response;
using SchoolSystem.Models.Request;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController(IDbConnection dbConnection) : ControllerBase
    {
        private readonly ResponseFactory _responseFactory = new(dbConnection);


        // GET: api/<TopicsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = "SELECT id, topic_name, description FROM topics";
            var response = await _responseFactory.QueryAsync<Topic>(query);
            
            return Ok(response);
        }

        [HttpGet("{count}/{offset}")]
        public async Task<IActionResult> GetFew(int count, int offset)
        {
            var query = "SELECT id, topic_name, description FROM topics LIMIT @quantity OFFSET @offset";
            var parameters = new { quantity = count, offset = count * offset };
            var response = await _responseFactory.QueryAsync<Topic>(query, parameters);

            return Ok(response);
        }

        // GET api/<TopicsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TopicsController>
        [HttpPost]
        public async Task<IActionResult> Post(TopicRequest topicRequest)
        {
            var query = "INSERT INTO topics (topic_name, description) VALUES (@topic_name, @description)";
            var parameters = new
            {
                topic_name = topicRequest.Topic_Name,
                description = topicRequest.Description
            };
            var response = await _responseFactory.ExecuteAsync(query, parameters);

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
