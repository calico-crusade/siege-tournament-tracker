namespace SiegeTournamentTracker.Api
{
    /// <summary>
    /// Represents an some item details
    /// </summary>
    public class LinkItem
    {
        /// <summary>
        /// The full name of the item
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// A short name for the item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The URL to get more details about this item
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// The image URL for this item
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Formats the item to a string
        /// </summary>
        /// <returns>The string format</returns>
        public override string ToString()
        {
            return $"{FullName}({Name}) - {Url} [{ImageUrl}]";
        }
    }
}
