namespace SiegeTournamentTracker.Api.MetaData
{
	/// <summary>
	/// A description of an enum
	/// </summary>
	public class EnumDescription
	{
		/// <summary>
		/// The value of the enum
		/// </summary>
		public int Value { get; set; }

		/// <summary>
		/// The code-specific name of the enum
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A display friendly version of the enum name
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// A description of the enum
		/// </summary>
		public string Description { get; set; }
	}
}
