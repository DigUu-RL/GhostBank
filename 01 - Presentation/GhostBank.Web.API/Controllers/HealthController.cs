using Microsoft.AspNetCore.Mvc;

namespace GhostBank.Web.API.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : Controller
{
	[HttpGet]
	public IActionResult Index()
	{
		return Ok("Relax, I'm healthy! :)");
	}
}
