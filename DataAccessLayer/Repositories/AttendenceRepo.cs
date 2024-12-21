using BuisnessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using MojoAuth.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class AttendenceRepo : IAttendence
    {
        private readonly AppDbContext _context;

        public AttendenceRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Attendence> CheckIn(string userID)
        {
            try
            {
                var user = await _context.Users.FindAsync(userID);
                if (user == null)
                {
                    throw new Exception($"User with ID {userID} not found.");
                }

                var attendence = new Attendence()
                {
                    UserId = userID,
                    Date = DateTime.UtcNow,
                    CheckIn = DateTime.UtcNow,
                    User = user,
                };

                _context.Attendence.Add(attendence);
                await _context.SaveChangesAsync(); 
                return attendence;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving attendance for user " + userID, ex);
            }
        }



        public async Task<Attendence> CheckOut(string userID)
        {
            var LatestCheckin = await _context.Attendence
                .Where(a => a.UserId == userID)
                .OrderByDescending(a => a.Date)
                .FirstOrDefaultAsync();
            if (LatestCheckin == null)
            {
                throw new Exception("No check-in record found for the user.");
            }
            if (LatestCheckin == null)
            {
                throw new Exception("No check-in record found for the user.");
            }

            LatestCheckin.CheckOut = DateTime.UtcNow;
            _context.Attendence.Update(LatestCheckin);
            await _context.SaveChangesAsync();  
            return LatestCheckin;
        }

        public async Task<List<Attendence>> GetAttendeceById(string id)
        {
            var attendence = await _context.Attendence
                .Where(x => x.UserId == id)
                .ToListAsync(); 
            return attendence;
        }
    }
}
