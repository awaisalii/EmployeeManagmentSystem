using BuisnessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using BuisnessLayer.HubConfig;
using BuisnessLayer.DTO.Request;
using BuisnessLayer.DTO.Response;

namespace DataAccessLayer.Repositories
{
    public class NotesRepo : INote
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHubContext<MyHub> _hubContext;
        private readonly IActivities _activities;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public NotesRepo(AppDbContext appDbContext, IHubContext<MyHub> hubContext , IActivities activities , UserManager<ApplicationUser> userManager , IMapper mapper  )
        {
            this._appDbContext = appDbContext;
            this._hubContext = hubContext;
            this._activities = activities;
            this._userManager = userManager;
            this._mapper = mapper;
        }
        public async Task<ServiceResponse> CreateNote(NotesRequest note )
        {
            try
            {
            var newNote= _mapper.Map<Notes>(note);
            
            _appDbContext.Notes.Add(newNote);
            await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            var activity = new Activities { };
            var user =await _userManager.FindByIdAsync(note.UserId);
            if (note.TaskId != null)
            {
                activity = new Activities
                {
                    Text = user?.FirstName +" " + user.LastName + " " + "Created a New Note",
                    UserId = user.Id,
                    TaskId = note.TaskId,
                    Date = DateTime.UtcNow,
                };
                await _activities.CreateActivity(activity);
            }
            else
            {
                activity = new Activities
                {
                    Text = user?.FirstName + " " + user.LastName + " " + "Created a New Note",
                    UserId = user.Id,
                    Date = DateTime.UtcNow,
                };
                await _activities.CreateActivity(activity);
                
            }
            return new ServiceResponse(true,"Saved Successfiully");
        }

        public async Task<List<Notes>> GetAllByTask(int id)
        {
           var result=await _appDbContext.Notes.Where(x=>x.TaskId==id).ToListAsync();
           return result;
        }

        public async Task<List<Notes>> GetAllByUser(string userId )
        {
            var result =await _appDbContext.Notes.Where(x => x.UserId == userId).ToListAsync();
            return result;
        }

        public async Task<Notes> GetById(int id)
        {
            var note =await _appDbContext.Notes.FirstOrDefaultAsync(x=>x.Id==id);
            return note;
        }


    }
}
