using BuisnessLayer.DTO.Request;
using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public interface IUser
    {
        Task<ServiceResponse> DeleteUser(string id);
        Task<UserDto> GetUser(string id , string requestScheme, string requestHost);
        Task<UserDto> UpdateUserAsync(UpdateUserRequest user);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<ServiceResponse> DeleteRole(string id);
    }
}
