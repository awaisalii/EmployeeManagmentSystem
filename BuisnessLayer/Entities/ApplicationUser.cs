using Microsoft.AspNetCore.Identity;

namespace BuisnessLayer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Position { get; set; }
        public bool? Salaried { get; set; }
        public string? Department { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime HiredDate { get; set; }
        public DateTime BirthDate { get; set; }
        public StatusSelectBox? Status { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Address { get; set; }
        public bool IsVerified { get; set; } = false;
        public string?  ImagePath { get; set; }
        public virtual ICollection<TaskModel>? Tasks { get; set; } = new HashSet<TaskModel>();
        public virtual ICollection<Notes> Notes { get; set; } = new HashSet<Notes>();
        public virtual ICollection<Activities> Activities { get; set; } = new HashSet<Activities>();
        public virtual ApplicationUser? AssignedTo { get; set; }
        public string? AssignedToId { get; set; }
        public int? GroupChatId { get; set; }
        public GroupChat GroupChat { get; set; }
        public virtual ICollection<ChatUser> ChatUsers { get; set; } = new HashSet<ChatUser>();
        public virtual ICollection<Messages> MessagesSent { get; set; } = new HashSet<Messages>(); // Navigation property for sent messages
    }

}
