using System.Collections.Generic;

namespace SiegeTournamentTracker.Api.MetaData
{
	/// <summary>
	/// Root object for the enum documentation
	/// </summary>
	public class EnumLocal
	{
		/// <summary>
		/// A collection of descriptions by locale / language code
		/// </summary>
		public Dictionary<string, List<EnumDescription>> Descriptions { get; set; }

		/// <summary>
		/// The type this description is for
		/// </summary>
		public string Type { get; set; }
	}
}
