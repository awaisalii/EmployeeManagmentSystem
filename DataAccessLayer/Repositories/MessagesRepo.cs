using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MessagesRepo : IMessages
    {
        private readonly AppDbContext _appDbContext;

        public MessagesRepo(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<MessageReponse> CreateGroupMessageMessage(int groupChatId, string user, string message)
        {
            var chatUser = _appDbContext.Users.Find(user);
            var newMessage = new Messages()
            {
                Sender=chatUser,
                SenderId =user,
                Content=message,
                Date=DateTime.UtcNow,
                GroupChatId= groupChatId
            };
            var result = await _appDbContext.Messages.AddAsync(newMessage);
            await _appDbContext.SaveChangesAsync();

            var addedMessage = result.Entity;

            var responseMessage = new MessageReponse
            {
                User = addedMessage.Sender.FirstName + " " + addedMessage.Sender.LastName,
                Date = addedMessage.Date,
                Message = addedMessage.Content,
                SenderId = addedMessage.SenderId,
                ImagePath=addedMessage.Sender.ImagePath
            };
            return responseMessage;

        }

        public Task<ServiceResponse> CreatMessage(Messages messages)
        {
            throw new NotImplementedException();
        }

        public Task<List<Messages>> GetAllByTask()
        {
            throw new NotImplementedException();
        }

        public Task<List<Messages>> GetAllByUser()
        {
            throw new NotImplementedException();
        }

        public Task<Messages> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
