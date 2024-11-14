using BuisnessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, RoleModel, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<TaskModel> TasksModel { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<PrivateChat> PrivateChats { get; set; }
        public DbSet<Activities> Activities { get; set; }
        public DbSet<Messages> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
       .HasOne(u => u.GroupChat)
       .WithMany()
       .HasForeignKey(u => u.GroupChatId)
       .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<TaskModel>()
               .HasOne(t => t.AssignedTo)
               .WithMany()
               .HasForeignKey(t => t.AssignedToId)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TaskModel>()
                .HasOne(t => t.CreatedBy)
                .WithMany()
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Notes>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Notes>()
               .HasOne(n => n.Task)
               .WithMany(t => t.Notes)
               .HasForeignKey(n => n.TaskId)
               .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<GroupChat>()
               .HasMany(g => g.Messages)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PrivateChat>()
               .HasMany(p => p.Messages)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ChatUser>()
               .HasKey(cu => cu.Id);
            modelBuilder.Entity<ChatUser>()
               .HasOne(cu => cu.ApplicationUser)
               .WithMany(u => u.ChatUsers)
               .HasForeignKey(cu => cu.ApplicationUserId);
            modelBuilder.Entity<ChatUser>()
               .HasOne(cu => cu.PrivateChat)
               .WithMany(pc => pc.ChatUsers)
               .HasForeignKey(cu => cu.PrivateChatId);
            modelBuilder.Entity<Messages>()
               .HasOne(m => m.PrivateChat)
               .WithMany(pc => pc.Messages)
               .HasForeignKey(m => m.PrivateChatId)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Messages>()
               .HasOne(m => m.Sender)
               .WithMany(u => u.MessagesSent)
               .HasForeignKey(m => m.SenderId)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<GroupChat>()
               .HasOne(gc => gc.AssociatedTask)
               .WithOne(t => t.GroupChat)
               .HasForeignKey<GroupChat>(gc => gc.TaskModelId);
            modelBuilder.Entity<Messages>()
               .HasOne(m => m.GroupChat)
               .WithMany(gc => gc.Messages)
               .HasForeignKey(m => m.GroupChatId)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<GroupChat>()
               .HasOne(gc => gc.AssociatedTask)
               .WithOne(t => t.GroupChat)
               .HasForeignKey<GroupChat>(gc => gc.TaskModelId);
        }

        internal async Task FindAsync(string taskId)
        {
            throw new NotImplementedException();
        }
    }
}
