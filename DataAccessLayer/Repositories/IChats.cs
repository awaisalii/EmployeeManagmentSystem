using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IChats
    {
        Task<List<MessageReponse>> GetPrivateChat(string receiverUserId, string senderId);
        Task<MessageReponse> CreatePrivateChat(string receiverUserId, string senderId, string message);
        Task DeletePrivateChat();
        Task UpdatePrivateChat();
        Task<List<MessageReponse>> GetGroupChat(int id);
        Task CreateGroupChat(GroupChat groupChat);
        Task DeleteGroupChat();
        Task UpdateGroupChat();
        Task<AllChatsResponse> GetChats(string id);
    }
}
