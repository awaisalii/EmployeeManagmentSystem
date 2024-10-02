using DataAccessLayer.Repositories;
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
            
        public MessagesController(IChats chats)
        {
            this._chats = chats;
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
    }
}
