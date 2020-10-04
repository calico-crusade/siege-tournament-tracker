using Microsoft.AspNetCore.Mvc;

namespace SiegeTournamentTracker.Web.Controllers
{
	/// <summary>
	/// MVC controller
	/// </summary>
	public class HomeController : Controller
	{
		/// <summary>
		/// The starting page for the application
		/// </summary>
		/// <returns>The index.html file in the wwwroot folder</returns>
		public IActionResult Index()
		{
			return File("index.html", "text/html");
		}
	}
}
