using BuisnessLayer.DTO.Request;
using BuisnessLayer.Entities;
using DataAccessLayer.Repositories;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentSystem.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITask _Itask;
        private readonly ItokenService _itokenService;

        public TasksController(ITask Itask, ItokenService ItokenService)
        {
            _Itask = Itask;
            _itokenService = ItokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskRequest task)
        {
            var tokenHeader = Request.Headers["Authorization"].ToString();
            var userData= _itokenService.GetToken(tokenHeader);
            if (userData == null)
            {
                return BadRequest();
            }
            var result = await _Itask.CreateAsync(task, userData.Id);
            return Ok(result);
        }


        [HttpGet("Task")]
        public async Task<IActionResult> GetById(string taskId)
        {
            var tokenHeader = Request.Headers["Authorization"].ToString();
            var userData = _itokenService.GetToken(tokenHeader);
            if (userData == null)
            {
                return BadRequest();
            }
            var userId = userData.Id;
            try
            {
                var result = await _Itask.GetById(taskId, userId);
               return Ok(result);
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpGet("GetUserOwnTasks")]
        public async Task<IActionResult> GetUserOwnTask()
        {
            var tokenHeader = Request.Headers["Authorization"].ToString();
            var userData = _itokenService.GetToken(tokenHeader);
            var userId = userData.Id;
            var result = await _Itask.GetUserTasks(userId);
            return Ok(result);
        }

        [HttpGet("UserTasks{id}")]
        public async Task<IActionResult> GetUserTasks(string id)
        {
            var result = await _Itask.GetUserTasks(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] TaskRequest task)
        {
            var token = Request.Headers["Authorization"].ToString();
            var userData = _itokenService.GetToken(token);
            if (userData == null)
            {
                return BadRequest();
            }
            var result = await _Itask.UpdateTask(task, userData.Id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            var token = Request.Headers["Authorization"].ToString();
            var userData = _itokenService.GetToken(token);
            var result = await _Itask.DeleteTaskAsync(id, userData.Id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
      {
            var result = await _Itask.GetAllAsync();
            return Ok(result);
        }

    }
}
