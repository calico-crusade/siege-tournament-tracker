using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiegeTournamentTracker.Api
{
	public interface ISiegeService
	{
		Task<Match[]> GetMatches();
		Task<Match[]> GetMatches(Func<Match, bool> filter);
		Task<Match[]> GetMatches(MatchStatus status);

		Task<string[]> Leagues();
	}

	public class SiegeService : ISiegeService
	{
		private static List<Match> _cachedMatches = null;
		private static DateTime? _cacheTime = null;

		public static int ExpireMin { get; set; } = 3;

		private readonly ITournamentApi _api;
		
		public SiegeService(ITournamentApi api)
		{
			_api = api;
		}

		public bool CacheExpired()
		{
			return _cachedMatches == null ||
				_cacheTime == null ||
				_cacheTime.Value.AddMinutes(ExpireMin) < DateTime.Now;
		}

		public async Task<Match[]> GetMatches()
		{
			if (CacheExpired())
			{
				_cachedMatches = (await _api
					.Matches())
					.Where(t => !string.IsNullOrEmpty(t.TeamOne.FullName) ||
								!string.IsNullOrEmpty(t.TeamTwo.FullName))
					.OrderBy(t => t.LocalTime)
					.ToList();
				_cacheTime = DateTime.Now;
			}

			return _cachedMatches.ToArray();
		}

		public async Task<Match[]> GetMatches(Func<Match, bool> filter)
		{
			var matches = await GetMatches();

			return matches.Where(filter).ToArray();
		}

		public Task<Match[]> GetMatches(MatchStatus status)
		{
			return GetMatches(t => t.Status == status);
		}

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
