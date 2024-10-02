using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using BuisnessLayer.HubConfig;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ActivitiesRepo : IActivities
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHubContext<MyHub> _hubContext;

        public ActivitiesRepo(AppDbContext appDbContext, IHubContext<MyHub> hubContext)
        {
            this._appDbContext = appDbContext;
            this._hubContext = hubContext;
        }
        public async Task<ServiceResponse> CreateActivity(Activities activities)
        {
            _appDbContext.Activities.Add(activities);
            await _appDbContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("Notification", $" {activities.Text}");
            return new ServiceResponse(true,"Activity Created Successfully");
        }

        public async Task<List<Activities>> GetAllByTask(int id)
        {
            var result=await _appDbContext.Activities.Where(x => x.TaskId == id).ToListAsync();
            return result;
        }

        public async Task<List<Activities>> GetAllByUser(string id)
        {
            var result = await _appDbContext.Activities.Where(x => x.UserId == id).ToListAsync();
            return result;
        }

        public async Task<List<Activities>> GetAll()
        {
            var result = _appDbContext.Activities
                .OrderByDescending(x=>x.Date)
                .ToList();
            return result;
        }


        public async Task<Activities> GetById(int id)
        {
            var result=await _appDbContext.Activities.Where(x=> x.Id == id).FirstOrDefaultAsync();
            return result;
        }
    }
}
