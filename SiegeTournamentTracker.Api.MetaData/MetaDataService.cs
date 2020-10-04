using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SiegeTournamentTracker.Api.MetaData
{
	/// <summary>
	/// A service for providing meta-data information for enum descriptions
	/// </summary>
	public interface IMetaDataService
	{
		/// <summary>
		/// Fetches the match status enum information
		/// </summary>
		/// <param name="local">The language locale (defaults to "en")</param>
		/// <returns>The collection of enum information</returns>
		IEnumerable<EnumDescription> MatchStatuses(string local = "en");
	}

	/// <summary>
	/// A service for providing meta-data information for enum descriptions
	/// </summary>
	public class MetaDataService : IMetaDataService
	{
		/// <summary>
		/// The cache of the locale information
		/// </summary>
		private static readonly Dictionary<string, EnumLocal> _localCache = new Dictionary<string, EnumLocal>();

		/// <summary>
		/// The base folder to fetch the information from
		/// </summary>
		private const string ENUM_FOLDER = "Descriptions";

		/// <summary>
		/// The format for the locale information file name
		/// </summary>
		private const string ENUM_FILE_FORMAT = "{0}.enumdesc.json";

		/// <summary>
		/// Fetches the current enum information from either the cache or the file
		/// </summary>
		/// <param name="filename">The file to fetch from</param>
		/// <returns>The enum information</returns>
		public EnumLocal FetchEnum(string filename)
		{
			var dir = Environment.CurrentDirectory;
			var path = Path.Combine(dir, ENUM_FOLDER, filename);

			if (_localCache.ContainsKey(path))
				return _localCache[path];

			if (!File.Exists(path))
				return null;

			var data = File.ReadAllText(path);
			var local = JsonConvert.DeserializeObject<EnumLocal>(data);

			_localCache.Add(path, local);
			return local;
		}

		/// <summary>
		/// Fetches the descriptions for the specified type and locales
		/// </summary>
		/// <param name="type">The type name</param>
		/// <param name="local">The language to fetch</param>
		/// <returns>A collection of the enum descriptions</returns>
		public IEnumerable<EnumDescription> FetchDescriptions(string type, string local)
		{
			var filename = string.Format(ENUM_FILE_FORMAT, type);
			var full = FetchEnum(filename);
			if (full == null ||
				full.Descriptions == null ||
				!full.Descriptions.ContainsKey(local))
				return null;

			return full.Descriptions[local];
		}

		/// <summary>
		/// Fetches the match status enum information
		/// </summary>
		/// <param name="local">The language locale (defaults to "en")</param>
		/// <returns>The collection of enum information</returns>
		public IEnumerable<EnumDescription> MatchStatuses(string local = "en")
		{
			return FetchDescriptions("MatchStatus", local);
		}
	}
}
