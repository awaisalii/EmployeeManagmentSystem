using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IAttendence
    {
        Task<List<Attendence>> GetAttendeceById(string id);
        Task<Attendence> CheckIn(string userID);
        Task<Attendence> CheckOut(string userID);
    }
}
