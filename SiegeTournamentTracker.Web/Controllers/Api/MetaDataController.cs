using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiegeTournamentTracker.Api;
using SiegeTournamentTracker.Api.MetaData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiegeTournamentTracker.Web.Controllers.Api
{
	/// <summary>
	/// API controller for fetching meta data information
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class MetaDataController : ControllerBase
	{
		/// <summary>
		/// The logger (DI)
		/// </summary>
		private readonly ILogger _logger;
		/// <summary>
		/// The meta data service (DI)
		/// </summary>
		private readonly IMetaDataService _meta;
		/// <summary>
		/// The image cache service (DI)
		/// </summary>
		private readonly IImageCacheService _image;
		/// <summary>
		/// The siege cache service (DI)
		/// </summary>
		private readonly ISiegeService _api;

		/// <summary>
		/// The dependency injected constructor
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="meta">The meta data service</param>
		/// <param name="image">The image cache service</param>
		/// <param name="api">The siege cache service</param>
		public MetaDataController(
			ILogger<MetaDataController> logger,
			IMetaDataService meta,
			IImageCacheService image,
			ISiegeService api)
		{
			_logger = logger;
			_meta = meta;
			_image = image;
			_api = api;
		}

		/// <summary>
		/// Fetches available metadata information for the <see cref="Api.MatchStatus"/> enum
		/// </summary>
		/// <param name="local"></param>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(500)]
		[ProducesResponseType(typeof(IEnumerable<EnumDescription>), 200)]
		public IActionResult MatchStatus(string local = "en")
		{
			try
			{
				var data = _meta.MatchStatuses(local);
				return Ok(data);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while fetching Match Status metadata");
				return StatusCode(500);
			}
		}

		/// <summary>
		/// Fetches the image by the given URL
		/// </summary>
		/// <param name="url">The url to fetch the image from</param>
		/// <returns>The image</returns>
		[HttpGet]
		[ProducesResponseType(500)]
		public async Task<IActionResult> Image(string url)
		{
			try
			{
				if (string.IsNullOrEmpty(url))
					return PhysicalFile("wwwroot/assets/r6-logo.png", "image/png");

				if (!url.ToLower().StartsWith(TournamentApi.BASE_URL.ToLower()))
					return NotFound();

				var image = await _image.GetImageFile(url);
				return PhysicalFile(image, "image/png");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while fetching image");
				return StatusCode(500);
			}
		}

		/// <summary>
		/// Fetches all of the leagues 
		/// </summary>
		/// <returns>The leagues</returns>
		[HttpGet]
		[ProducesResponseType(500)]
		[ProducesResponseType(typeof(IEnumerable<string>), 200)]
		public async Task<IActionResult> Leagues()
		{
			try
			{
				var leagues = await _api.Leagues();
				return Ok(leagues);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while fetching leagues.");
				return StatusCode(500);
			}
		}
	}
}
