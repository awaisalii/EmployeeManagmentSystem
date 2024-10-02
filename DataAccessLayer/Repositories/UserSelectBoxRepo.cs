using BuisnessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserSelectBoxRepo : ISelectBox
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSelectBoxRepo(UserManager<ApplicationUser> userManager )
        {
            this._userManager = userManager;
        }
        public async Task<List<UserSelectBoxModel>> GetUserSelectBox()
        {
            var users = _userManager.Users;

            var result = users.Select(users=> new UserSelectBoxModel
            {
                Id=users.Id,
                UserName=users.FirstName+" "+users.LastName,
            }).ToList();

            return result;
        }
}
}
