using BuisnessLayer.Entities;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BuisnessLayer.HubConfig
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, List<string>> connectedUsers = new Dictionary<string, List<string>>();
        private readonly IChats _chats;
        private readonly IMessages _messages;

        public ChatHub(IChats chats, IMessages messages ) 
        {
            
            this._chats = chats;
            this._messages = messages;
        }
        public async Task JoinTaskGroup(string taskId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, taskId);
        }
        public async Task SendMessageToTaskGroup(int groupChatId, string message)
        {
            var senderUserId = Context.UserIdentifier;
            var newMessage= await _messages.CreateGroupMessageMessage(groupChatId, senderUserId, message);
            await Clients.Group((groupChatId).ToString()).SendAsync("ReceiveGroupMessage", newMessage);
        }
        public async Task SendPrivateMessage(string receiverUserId, string message)
        {
            var senderUserId = Context.UserIdentifier;
            var result=await _chats.CreatePrivateChat(receiverUserId,senderUserId,message);
            if (connectedUsers.TryGetValue(receiverUserId, out var connectionIds))
            {
                foreach (var connectionId in connectionIds)
                {
                    if (connectionId!=null)
                    {
                    var user = Context.User.Identity.Name;
                    await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", result);
                    }
                }
            }

        }   
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;

            if (string.IsNullOrEmpty(userId))
            {
                Console.WriteLine("User identifier is null. User may not be authenticated.");
                return; 
            }

            if (!connectedUsers.ContainsKey(userId))
            {
                connectedUsers[userId] = new List<string>();
            }
            connectedUsers[userId].Add(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId) && connectedUsers.ContainsKey(userId))
            {
                connectedUsers[userId].Remove(Context.ConnectionId);
                if (connectedUsers[userId].Count == 0)
                {
                    connectedUsers.Remove(userId);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

    }

}
