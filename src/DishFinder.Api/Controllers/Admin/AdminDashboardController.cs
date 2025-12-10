using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboardService _adminDashboardService;

        public AdminDashboardController(IAdminDashboardService adminDashboardService)
        {
              _adminDashboardService = adminDashboardService;
        }

        [HttpGet("GetAllStats")]
        public async Task<IActionResult> GetAllStats(CancellationToken cancellationToken)
        {
            return Ok(await _adminDashboardService.GetAllStatsAsync(cancellationToken));
        }
    }
}
