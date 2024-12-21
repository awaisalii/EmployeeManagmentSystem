using AutoMapper;
using AutoMapper.Execution;
using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ChatsRepo : IChats
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ChatsRepo(AppDbContext appDbContext,IMapper mapper)
        {
            this._appDbContext = appDbContext;
            this._mapper = mapper;
        }
        public Task CreateGroupChat(GroupChat groupChat )
        {
            _appDbContext.GroupChats.Add(groupChat);
            _appDbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<MessageReponse> CreatePrivateChat(string receiverUserId,string senderId, string message)
        {
            var user1 =await _appDbContext.Users.FindAsync(receiverUserId);
            var user2 = await _appDbContext.Users.FindAsync(senderId);

            if (user1 == null || user2 == null)
            {
                throw new Exception("One or both users not found.");
            }
            PrivateChat privateChat;
            var existingChat = await _appDbContext.PrivateChats
                    .Include(pc => pc.ChatUsers)
                    .ThenInclude(cu => cu.ApplicationUser)
                    .Where(pc => pc.ChatUsers.Any(cu => cu.ApplicationUserId == receiverUserId) &&
                                 pc.ChatUsers.Any(cu => cu.ApplicationUserId == senderId))
                    .FirstOrDefaultAsync();
            if (existingChat != null)
            {
                privateChat = existingChat;
            }
            else
            {
            privateChat = new PrivateChat
                {
                    ChatUsers = new List<ChatUser> {
                            new ChatUser { ApplicationUser = user1, ApplicationUserId = user1.Id },
                            new ChatUser { ApplicationUser = user2, ApplicationUserId = user2.Id }
                },
             };
            _appDbContext.PrivateChats.Add(privateChat);
            await _appDbContext.SaveChangesAsync();
            }
            var newMessage = new Messages
            {
                PrivateChatId = privateChat.Id,
                Date = DateTime.UtcNow,
                SenderId = user2.Id,
                Content = message,
            };
            await _appDbContext.Messages.AddAsync(newMessage);
            await _appDbContext.SaveChangesAsync();
            var messageResponse = new MessageReponse
            {
                User = newMessage.Sender.FirstName + " " + newMessage.Sender.LastName,
                Date = newMessage.Date,
                Message=newMessage.Content,
                ImagePath=newMessage.Sender.ImagePath,
            };

            return messageResponse;
        }

        public Task DeleteGroupChat()
        {
            throw new NotImplementedException();
        }

        public Task DeletePrivateChat()
        {
            throw new NotImplementedException();
        }

        public async Task<List<MessageReponse>> GetGroupChat(int id)
        {
            var messages= await _appDbContext.Messages
                .Where(m => m.GroupChatId == id)
                .Include(x=>x.Sender)
                .OrderBy(m => m.Date)
                .ToListAsync();
            var result = messages
                .Where(m=>m.Sender!=null)
                .Select(m => 
            new MessageReponse
            {
                User = m.Sender.FirstName + " " + m.Sender?.LastName,
                Date = m.Date,
                Message = m.Content,
                SenderId = m.SenderId,
                ImagePath=m.Sender.ImagePath,
            }).ToList();
            return result;
        }

        public async Task<AllChatsResponse> GetChats(string id)
        {
            var privateChats = await _appDbContext.PrivateChats
                .Where(x => x.ChatUsers.Any(cu => cu.ApplicationUserId == id))
                .Include(x => x.ChatUsers)
                .ToListAsync();
            var allChatUserIds = privateChats
                .SelectMany(chat => chat.ChatUsers)
                .Select(cu => cu.ApplicationUserId)
                .Distinct()
                .Where(userId => userId != id)
                .ToList();
            var users = _appDbContext.Users
                .Where(x => allChatUserIds.Contains(x.Id))
                .ToList();
            var userResponse = _mapper.Map<List<UserDto>>(users);
            var groupChats = _appDbContext.GroupChats
                .Where(x => x.TaskModelId == 0)
                .ToList();
            var PrivateChatResponse = new PrivateChatResponse()
            {
                ChatsUsers = userResponse
            };
            var chats = new AllChatsResponse()
            {
                PrivateChats = PrivateChatResponse,
                GroupChats = groupChats 
            };
            return chats; 
        }


        public async Task<List<MessageReponse>> GetPrivateChat(string receiverUserId, string senderId)
        {
            var user1 = await _appDbContext.Users.FindAsync(receiverUserId);
            var user2 = await _appDbContext.Users.FindAsync(senderId);

            if (user1 == null || user2 == null)
            {
                throw new Exception("One or both users not found.");
            }
            var existingChat = await _appDbContext.PrivateChats
                .Include(pc => pc.ChatUsers)
                .ThenInclude(cu => cu.ApplicationUser)
                .Where(pc => pc.ChatUsers.Any(cu => cu.ApplicationUserId == receiverUserId) &&
                             pc.ChatUsers.Any(cu => cu.ApplicationUserId == senderId))
                .FirstOrDefaultAsync();
            if (existingChat == null)
            {
                return new List<MessageReponse>();
            }
            var messages = await _appDbContext.Messages
                .Where(m => m.PrivateChatId == existingChat.Id)
                .OrderBy(m => m.Date)
                .ToListAsync();
            var messageResponses = messages.Select(m => new MessageReponse
            {
                User = m.Sender.FirstName + " " + m.Sender.LastName,
                Date = m.Date,
                Message = m.Content,
                SenderId=m.SenderId,
                ImagePath= m.Sender.ImagePath
            }).ToList();

            return messageResponses;
        }




        public Task UpdateGroupChat()
        {
            throw new NotImplementedException();
        }

        public Task UpdatePrivateChat()
        {
            throw new NotImplementedException();
        }

        
    }
}
