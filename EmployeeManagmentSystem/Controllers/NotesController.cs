using BuisnessLayer.DTO.Request;
using BuisnessLayer.Entities;
using DataAccessLayer.Repositories;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentSystem.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class NotesController : Controller
    {
        private readonly INote _iNote;
        private readonly ItokenService _itokenService;

        public NotesController(INote note , ItokenService itokenService)
        {
            this._iNote = note;
            this._itokenService = itokenService;
        }

        [HttpGet("TaskNotes{id}")]
        public async Task<IActionResult> GetTaskNotes(int id) 
        {
            var result =await _iNote.GetAllByTask(id);
            return Ok(result);
        }
            
        [HttpPost]
        public async Task<IActionResult> CreateNote(NotesRequest note)
        {
            
            var token = Request.Headers["Authorization"].ToString();
            var userData = _itokenService.GetToken(token);
            if(userData ==null)
            {
                return BadRequest();
            }
            else
            {
                note.UserId = userData.Id;
            }
            var result =await _iNote.CreateNote(note);
            return Ok(result);
        }

        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetUserNotes(string id)
        {
            var result =await _iNote.GetAllByUser(id);
            return Ok(result);
        }

        [HttpGet("GetById{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result =await _iNote.GetById(id);
            return Ok(result);
        }
    }
}
