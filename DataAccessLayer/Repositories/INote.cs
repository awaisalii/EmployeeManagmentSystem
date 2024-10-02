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
    public interface INote
    {
        Task<Notes> GetById(int id);    
        Task<List<Notes>> GetAllByTask(int id);
        Task<List<Notes>> GetAllByUser(string userId);
        Task<ServiceResponse> CreateNote(NotesRequest note);
    }
}
