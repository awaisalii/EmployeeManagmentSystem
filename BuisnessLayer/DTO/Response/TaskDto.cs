using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnessLayer.DTO.Request;
using BuisnessLayer.Entities;

namespace BuisnessLayer.DTO.Response
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? UserId { get; set; } = null;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public string Progress { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
        public string CreatedBy { get; set; }
        public string AssignedToId { get; set; }
        public string CreatedById { get; set; }
        public int GroupChatId { get; set; }
        public List<NotesDto> Notes { get; set; }
        public List<ActivitiesRequest> Activities { get; set; }
    }
}
