using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Entities
{
    public class GroupChat
    {
        public int Id { get; set; }
        public int TaskModelId { get; set; }
        public TaskModel AssociatedTask { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public ICollection<ApplicationUser> ChatUsers { get; set; }= new HashSet<ApplicationUser>();  
        public ICollection<Messages> Messages { get; set; } = new HashSet<Messages>();
    }
}
