using DataAccessLayer.Repositories;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagmentSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : Controller
    {
        private readonly IChats _chats;
        private readonly ItokenService _itokenService;
        public MessagesController(IChats chats,ItokenService itokenService) 
        {
            this._chats = chats;
            _itokenService = itokenService;
        }



        [HttpGet]
        public async Task<IActionResult> GetMessages(string reciver)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
             var result=await _chats.GetPrivateChat(reciver, userId);
            return Ok(result);
        }
        [HttpGet("GroupMessages")]
        public async Task<IActionResult> GetGroupMessages(int id)
        {
            var result = await _chats.GetGroupChat(id);
            return Ok(result);
        }

        [HttpGet("GetALLChats")]
        public async Task<IActionResult> GetAllChats()
        {
            var tokenHeader = Request.Headers["Authorization"].ToString();
            var userData = _itokenService.GetToken(tokenHeader);
            var result = await _chats.GetChats(userData.Id);
            return Ok(result);
        }
    }
}
