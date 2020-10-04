using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiegeTournamentTracker.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiegeTournamentTracker.Web.Controllers.Api
{
	/// <summary>
	/// API controller for fetching match information
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class MatchesController : ControllerBase
	{
		/// <summary>
		/// The logger (DI)
		/// </summary>
		private readonly ILogger _logger;
		/// <summary>
		/// The siege service (DI)
		/// </summary>
		private readonly ISiegeService _api;

		/// <summary>
		/// The dependency injected constructor
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="api">The siege cache service</param>
		public MatchesController(ILogger<MatchesController> logger, ISiegeService api)
		{
			_logger = logger;
			_api = api;
		}

		/// <summary>
		/// Fetches all of the recent and upcoming siege matches
		/// </summary>
		/// <returns>All of the recent and upcoming siege matches</returns>
		[HttpGet]
		[ProducesResponseType(500)]
		[ProducesResponseType(typeof(IEnumerable<Match>), 200)]
		public async Task<IActionResult> All()
		{
			try
			{
				var matches = await _api.GetMatches();
				return Ok(matches);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while fetching matches");
				return StatusCode(500);
			}
		}

		/// <summary>
		/// Fetches any upcoming matches
		/// </summary>
		/// <returns>All of the upcoming matches</returns>
		[HttpGet]
		[ProducesResponseType(500)]
		[ProducesResponseType(typeof(IEnumerable<Match>), 200)]
		public async Task<IActionResult> Upcoming()
		{
			try
			{
				var matches = await _api.GetMatches(MatchStatus.Upcoming);
				return Ok(matches);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while fetching upcoming matches");
				return StatusCode(500);
			}
		}
	}
}
