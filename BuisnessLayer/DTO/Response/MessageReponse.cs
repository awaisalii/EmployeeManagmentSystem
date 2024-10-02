using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.DTO.Response
{
    public class MessageReponse
    {
        public string User { get; set; }
        public string? Subject { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string SenderId { get; set; }
        public string? ImagePath { get; set; }
    }
}
