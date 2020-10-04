using System.Collections.Generic;

namespace SiegeTournamentTracker.Api.MetaData
{
	public class EnumLocal
	{
		public Dictionary<string, List<EnumDescription>> Descriptions { get; set; }
		public string Type { get; set; }
	}
}
