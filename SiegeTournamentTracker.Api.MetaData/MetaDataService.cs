using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SiegeTournamentTracker.Api.MetaData
{
	public interface IMetaDataService
	{
		IEnumerable<EnumDescription> MatchStatuses(string local = "en");
	}

	public class MetaDataService : IMetaDataService
	{
		private static readonly Dictionary<string, EnumLocal> _localCache = new Dictionary<string, EnumLocal>();
		private const string ENUM_FOLDER = "Descriptions";
		private const string ENUM_FILE_FORMAT = "{0}.enumdesc.json";

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

		public IEnumerable<EnumDescription> MatchStatuses(string local = "en")
		{
			return FetchDescriptions("MatchStatus", local);
		}
	}
}
