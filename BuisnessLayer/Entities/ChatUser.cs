using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Entities
{
    public class ChatUser
    {
        public int Id { get; set; }
        public int PrivateChatId { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public PrivateChat PrivateChat { get; set; }
    }
}
