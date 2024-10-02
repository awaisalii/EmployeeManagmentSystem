

using Microsoft.AspNetCore.SignalR;

namespace BuisnessLayer.HubConfig
{
    public class MyHub :Hub
    {
           private static readonly Dictionary<string, string> _userConnections = new Dictionary<string, string>();
        public async Task Notification(string someTextFromClient)
        {

            string tempString;
            if (someTextFromClient == "hey")
            {
                tempString = "message way Hey";
            }
            else
            {
                tempString = "Message was somthing else";
            }
            await Clients.Clients(this.Context.ConnectionId).SendAsync("Notification", tempString) ;
        }
        public async Task NotifyUser(string userId, string noteText)
        {
            await Clients.User(userId).SendAsync("Notification", $"New note created: {noteText}");
        }
        
    }
}
