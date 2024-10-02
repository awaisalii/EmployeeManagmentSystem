using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IMessages
    {
        Task<Messages> GetById(int id);
        Task<List<Messages>> GetAllByTask();
        Task<List<Messages>> GetAllByUser();
        Task<ServiceResponse> CreatMessage(Messages messages);
        Task<MessageReponse> CreateGroupMessageMessage(int groupChatId, string user, string message);
    }
}
