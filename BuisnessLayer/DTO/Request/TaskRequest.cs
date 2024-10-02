using BuisnessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.DTO.Request
{
    public class TaskRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? UserId { get; set; } = null;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public string? Progress { get; set; }
        public string Status { get; set; }
        public string? assignedTo { get; set; }
        public string? AssignedToId { get; set; }
        public string? CreatedBy { get; set; }
    }
}
