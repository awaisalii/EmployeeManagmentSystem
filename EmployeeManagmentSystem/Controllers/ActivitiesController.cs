using BuisnessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentSystem.Controllers
{
    [Authorize("Role=HR")]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : Controller
    {
        private readonly IActivities _activities;

        public ActivitiesController(IActivities activities)
        {
            this._activities = activities;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =await _activities.GetAll();
            return Ok(result);
        }
    }
}
