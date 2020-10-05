using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiegeTournamentTracker.Api
{
	/// <summary>
	/// Service for caching the results of the <see cref="TournamentApi.Matches"/> request
	/// </summary>
	public interface ISiegeService
	{
		/// <summary>
		/// Gets all of the matches from either the cache or liquipedia if the cache is expired
		/// </summary>
		/// <returns>All of the matches</returns>
		Task<Match[]> GetMatches();
		/// <summary>
		/// Filters through the results of the <see cref="GetMatches"/>
		/// </summary>
		/// <param name="filter">The filter to use</param>
		/// <returns>The filtered collection of matches</returns>
		Task<Match[]> GetMatches(Func<Match, bool> filter);
		/// <summary>
		/// Filters through the results of <see cref="GetMatches"/> and fetches only the ones with the given status
		/// </summary>
		/// <param name="status">The status to filter for</param>
		/// <returns>The filtered collection of matches</returns>
		Task<Match[]> GetMatches(MatchStatus status);

		/// <summary>
		/// Fetches a collection of all of the leagues in all of the matches
		/// </summary>
		/// <returns>A distinct list of all leagues</returns>
		Task<string[]> Leagues();
	}

	/// <summary>
	/// Service for caching the results of the <see cref="TournamentApi.Matches"/> request
	/// </summary>
	public class SiegeService : ISiegeService
	{
		/// <summary>
		/// The current cache of the matches
		/// </summary>
		private static List<Match> _cachedMatches = null;
		/// <summary>
		/// When the cache was created
		/// </summary>
		private static DateTime? _cacheTime = null;

		/// <summary>
		/// How many minutes the server should wait before requesting a new batch of matches
		/// </summary>
		public static int ExpireMin { get; set; } = 3;

		/// <summary>
		/// The tournament API instance
		/// </summary>
		private readonly ITournamentApi _api;
		
		/// <summary>
		/// Dependency Injected Constructor
		/// </summary>
		/// <param name="api">The tournament api instance to be injected</param>
		public SiegeService(ITournamentApi api)
		{
			_api = api;
		}

		/// <summary>
		/// Whether or not the current cache is expired
		/// </summary>
		/// <returns>Whether or not the current cache is expired</returns>
		public bool CacheExpired()
		{
			return _cachedMatches == null ||
				_cacheTime == null ||
				_cacheTime.Value.AddMinutes(ExpireMin) < DateTime.Now;
		}

		/// <summary>
		/// Gets all of the matches from either the cache or liquipedia if the cache is expired
		/// </summary>
		/// <returns>All of the matches</returns>
		public async Task<Match[]> GetMatches()
		{
			if (CacheExpired())
			{
				_cachedMatches = (await _api
					.Matches())
					.Where(t => !string.IsNullOrEmpty(t.TeamOne.FullName) ||
								!string.IsNullOrEmpty(t.TeamTwo.FullName))
					.OrderBy(t => t.Timestamp)
					.ToList();
				_cacheTime = DateTime.Now;
			}

			return _cachedMatches.ToArray();
		}

		/// <summary>
		/// Filters through the results of the <see cref="GetMatches"/>
		/// </summary>
		/// <param name="filter">The filter to use</param>
		/// <returns>The filtered collection of matches</returns>
		public async Task<Match[]> GetMatches(Func<Match, bool> filter)
		{
			var matches = await GetMatches();

			return matches.Where(filter).ToArray();
		}

		/// <summary>
		/// Filters through the results of <see cref="GetMatches"/> and fetches only the ones with the given status
		/// </summary>
		/// <param name="status">The status to filter for</param>
		/// <returns>The filtered collection of matches</returns>
		public Task<Match[]> GetMatches(MatchStatus status)
		{
			return GetMatches(t => t.Status == status);
		}

		/// <summary>
		/// Fetches a collection of all of the leagues in all of the matches
		/// </summary>
		/// <returns>A distinct list of all leagues</returns>
		public async Task<string[]> Leagues()
		{
			return (await GetMatches())
				.Select(t => t?.League?.Name)
				.Where(t => !string.IsNullOrEmpty(t))
				.Distinct()
				.OrderBy(t => t)
				.ToArray();
		}
	}
}
