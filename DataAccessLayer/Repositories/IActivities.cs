using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IActivities
    {
        Task<Activities> GetById(int id);
        Task<List<Activities>> GetAllByTask(int id);
        Task<List<Activities>> GetAllByUser(string id);
        Task<ServiceResponse> CreateActivity(Activities activities);
        Task<List<Activities>> GetAll();
    }
}
