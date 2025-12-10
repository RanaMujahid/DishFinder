using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers
{
    [Route("areas")]
    [ApiController]
    public class AreasController : ControllerBase
    {


        [HttpGet("popular")]
        public async Task<IActionResult> Popular(CancellationToken cancellationToken)
        {
            var areas = new[]
            {
                "Downtown Dubai",
                "Jumeirah",
                "Marina",
                "Deira",
                "DIFC",
                "JLT",
                "Business Bay",
                "Al Barsha"
            };

            return Ok(areas);
        }
    }
}
