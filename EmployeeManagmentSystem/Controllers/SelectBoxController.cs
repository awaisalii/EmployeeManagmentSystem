using BuisnessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentSystem.Controllers
{
    [Authorize]
    [Route("/api")]
    [ApiController]
    public class SelectBoxController : Controller
    {
        private readonly ISelectBox _selectBox;

        public SelectBoxController(ISelectBox selectBox)
        {
            this._selectBox = selectBox;
        }

        [HttpGet("UserSelectBox")]
        public async Task<List<UserSelectBoxModel>> UserSelectBox()
        {
            var result = await _selectBox.GetUserSelectBox();
            return result;
        }
    }
}
