using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentSystem.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet]
        public Task<IActionResult> Result()
        {
            var result = "Hello World";
            return Task.FromResult<IActionResult>(Ok(result));
        }


    }
}
