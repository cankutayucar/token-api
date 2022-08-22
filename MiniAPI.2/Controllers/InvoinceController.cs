using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MiniAPI._2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoinceController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetInvoince()
        {
            var userName = HttpContext.User.Identity?.Name;
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return Ok($"invoince islemleri => username : {userName} - userid : {userId?.Value}");
        }
    }
}
