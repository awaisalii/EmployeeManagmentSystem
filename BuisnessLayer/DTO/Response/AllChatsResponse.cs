using BuisnessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.DTO.Response
{
    public class AllChatsResponse
    {
        public PrivateChatResponse? PrivateChats { get; set; } 
        public List<GroupChat>? GroupChats { get; set; } 
    }
}
