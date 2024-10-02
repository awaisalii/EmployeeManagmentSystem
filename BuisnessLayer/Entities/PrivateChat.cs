using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Entities
{
    public class PrivateChat
    {
        public int Id { get; set; }
        public virtual ICollection<ChatUser> ChatUsers { get; set; } = new HashSet<ChatUser>();
        public virtual ICollection<Messages>? Messages { get; set; } = new HashSet<Messages>();
    }
}
