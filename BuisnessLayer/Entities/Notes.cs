using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Entities
{
    public class Notes
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public virtual TaskModel? Task { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public int? TaskId { get; set; }
    }
}
