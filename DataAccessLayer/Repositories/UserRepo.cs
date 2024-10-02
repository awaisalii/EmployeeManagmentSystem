using AutoMapper;
using BuisnessLayer.DTO;
using BuisnessLayer.DTO.Request;
using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace DataAccessLayer.Repositories
{
    public class UserRepo : IUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<RoleModel> _roleManager;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UserRepo(UserManager<ApplicationUser> userManager, RoleManager<RoleModel> roleManager , AppDbContext appDbContext, IMapper mapper )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._appDbContext = appDbContext;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ServiceResponse(false, "User Not Found");
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new ServiceResponse(true, "User Has been Deleted successfully");
            }
            return new ServiceResponse(false, "Error");
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task<UserDto> GetUser(string id, string requestScheme, string requestHost)
            {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _appDbContext.Entry(user).Collection(u => u.Tasks).LoadAsync();
                await _appDbContext.Entry(user).Collection(u => u.Notes).LoadAsync();
                await _appDbContext.Entry(user).Collection(u => u.Activities).LoadAsync();
                string imagePath;

                if (!string.IsNullOrEmpty(user.ImagePath))
                {
                    imagePath = $"{requestScheme}://{requestHost}/uploads/images/{Uri.EscapeDataString(user.ImagePath)}";
                }
                else
                {
                    imagePath = $"{requestScheme}://{requestHost}/uploads/images/nouser.jpeg";
                }

                var assignedToUser=_userManager.Users.Where(x=>x.Id == user.AssignedToId).FirstOrDefault();

                var userDto=_mapper.Map<UserDto>(user);
                userDto.ImagePath = imagePath;
                var newNotes= userDto.Notes.OrderByDescending(note => note.Date).ToList();
                var ReversedActivities = userDto.Activities.OrderByDescending(activity => activity.Date).ToList();
                userDto.Activities = ReversedActivities;
                userDto.Notes = newNotes;

                return userDto;
            }
            return null;
        }



        public async Task<UserDto> UpdateUserAsync(UpdateUserRequest user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);

            string FileName = "";
            if (user.Image != null)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                FileName = Guid.NewGuid().ToString() + "_" + user.Image.FileName;
                string filePath = Path.Combine(folderPath, FileName);
                user.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                existingUser.ImagePath = FileName;
            }
            if (!string.IsNullOrEmpty(user.AssignedToId) && user.AssignedToId != "null")
            {
                var assignedToUser = _userManager.Users
                    .FirstOrDefault(x => x.Id == user.AssignedToId);

                if (assignedToUser != null)
                {
                    existingUser.AssignedTo = assignedToUser;
                    existingUser.AssignedToId = assignedToUser.Id;
                }
            }


            existingUser.UserName = !string.IsNullOrEmpty(user?.UserName) ? user.UserName : existingUser.UserName;
            existingUser.Email = !string.IsNullOrEmpty(user?.Email) ? user.Email : existingUser.Email;
            existingUser.Position = !string.IsNullOrEmpty(user?.Position) ? user.Position : existingUser.Position;
            existingUser.Department = !string.IsNullOrEmpty(user?.Department) ? user.Department : existingUser.Department;
            existingUser.PasswordHash = !string.IsNullOrEmpty(user?.PasswordHash) ? user.PasswordHash : existingUser.PasswordHash;
            existingUser.PhoneNumber = !string.IsNullOrEmpty(user?.PhoneNumber) ? user.PhoneNumber : existingUser.PhoneNumber;
            existingUser.EmailConfirmed = existingUser.EmailConfirmed || user.EmailConfirmed;
            existingUser.Salaried = user.Salaried ?? existingUser.Salaried;
            existingUser.FirstName = !string.IsNullOrEmpty(user?.FirstName) ? user.FirstName : existingUser.FirstName;
            existingUser.LastName = !string.IsNullOrEmpty(user?.LastName) ? user.LastName : existingUser.LastName;
            existingUser.HiredDate = user.HiredDate ?? existingUser.HiredDate; 
            existingUser.BirthDate = user.BirthDate ?? existingUser.BirthDate;
            existingUser.Status = user.Status==null ? existingUser.Status : (StatusSelectBox)user.Status  ;
            existingUser.Country = !string.IsNullOrEmpty(user?.Country) ? user.Country : existingUser.Country;
            existingUser.Address = !string.IsNullOrEmpty(user?.Address) ? user.Address : existingUser.Address;
            existingUser.State = !string.IsNullOrEmpty(user?.State) ? user.State : existingUser.State;
            existingUser.City = !string.IsNullOrEmpty(user?.City) ? user.City : existingUser.City;
            

            var result = await _userManager.UpdateAsync(existingUser);
            if (result.Succeeded)
            {
                var response = _mapper.Map<UserDto>(result);
                return response;
            }
            return null;
        }



        public async Task<ServiceResponse> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                new ServiceResponse(false, "Role Does not Exiist");
            }
            var result =await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return new ServiceResponse(true, "Role Deleted Successfully");
            }
            return new ServiceResponse(false, "Error");

        }

        
    }
}
