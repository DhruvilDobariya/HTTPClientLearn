using HTTPClientFactoryDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HTTPClientFactoryDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TodoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://jsonplaceholder.typicode.com/todos");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponceMessage = await httpClient.SendAsync(httpRequestMessage);

            if(httpResponceMessage.IsSuccessStatusCode)
            {
                List<Todo> todo = JsonConvert.DeserializeObject<List<Todo>>(httpResponceMessage.Content.ReadAsStringAsync().Result);

                return Ok(todo);
            }
            else
            {
                return NotFound(httpResponceMessage.StatusCode + " " + httpResponceMessage.ReasonPhrase);
            }
        }
    }
}
