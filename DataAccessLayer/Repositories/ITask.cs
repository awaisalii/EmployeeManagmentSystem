using BuisnessLayer.DTO.Request;
using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface ITask
    {
        Task<List<TaskDto>> GetAllAsync();
        Task<TaskDto> GetById(string taskId, string userId);
        Task<IEnumerable<TaskModel>> GetUserTasks( string userId);
        Task<ServiceResponse> DeleteTaskAsync(string taskId, string userId);
        Task<ServiceResponse>  UpdateTask(TaskRequest model, string userId);
        Task<ServiceResponse> CreateAsync(TaskRequest model, string Id);
    }
}
