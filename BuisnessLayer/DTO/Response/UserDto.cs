using BuisnessLayer.DTO.Request;
using BuisnessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.DTO.Response
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<TaskDto>? Tasks { get; set; }
        public string? Position { get; set; }
        public bool? Salaried { get; set; }
        public string? Department { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? HiredDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Status { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public ICollection<NotesDto>? Notes { get; set; }
        public List<ActivitiesRequest>? Activities { get; set; }
        public string? AssignedTo { get; set; }
        public string? AssignedToId { get; set; }
    }
}
