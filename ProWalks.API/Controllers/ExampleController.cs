using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllExamples()
        {
            string[] allExamples = new string[] { "Example1", "Example2", "Example3", "Example4" };

            return Ok(allExamples);
        }
    }
}
