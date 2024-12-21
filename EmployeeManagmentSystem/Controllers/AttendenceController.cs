using DataAccessLayer.Repositories;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AttendenceController : Controller
    {
        private readonly IAttendence _attendence;
        private readonly ItokenService _itokenService;

        public AttendenceController(IAttendence attendence,ItokenService itokenService)
        {
            this._attendence = attendence;
            this._itokenService = itokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string id)
        {
            var result =await _attendence.GetAttendeceById(id);
            return Ok(result);
        }

        [HttpGet("Emp/Attendence")]
        public async Task<IActionResult> GetMy()
        {
            var token = Request.Headers["Authorization"].ToString();
            var userData = _itokenService.GetToken(token);
            var result = await _attendence.GetAttendeceById(userData.Id);
            return Ok(result);
        }

        [HttpGet("checkin")]
        public async Task<IActionResult> CheckIn()
        {
            var token = Request.Headers["Authorization"].ToString();
            var userData= _itokenService.GetToken(token);
            var result =await _attendence.CheckIn(userData.Id);
            return Ok();
        }

        [HttpGet("checkout")]
        public async Task<IActionResult> CheckOut()
        {
            var token = Request.Headers["Authorization"].ToString();
            var userData = _itokenService.GetToken(token);
            var result =await _attendence.CheckOut(userData.Id);
            return Ok();
        }

    }
}
