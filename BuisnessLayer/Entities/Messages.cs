using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BuisnessLayer.Entities
{
    public class Messages
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }
        [JsonIgnore]
        public int? PrivateChatId { get; set; }
        [JsonIgnore]
        public int? GroupChatId { get; set; }
        [JsonIgnore]
        public virtual PrivateChat PrivateChat { get; set; }
        [JsonIgnore]
        public virtual GroupChat GroupChat { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser Sender { get; set; }
    }
}
