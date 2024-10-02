using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnessLayer.Entities;
namespace DataAccessLayer.Repositories
{
    public interface ISelectBox
    {
        Task<List<UserSelectBoxModel>> GetUserSelectBox();
    }
}
