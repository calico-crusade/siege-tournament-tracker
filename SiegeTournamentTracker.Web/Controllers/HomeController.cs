using Microsoft.AspNetCore.Mvc;

namespace SiegeTournamentTracker.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return File("index.html", "text/html");
		}
	}
}
