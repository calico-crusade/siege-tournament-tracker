namespace SiegeTournamentTracker.Api
{
    public class LinkItem
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }

        public override string ToString()
        {
            return $"{FullName}({Name}) - {Url} [{ImageUrl}]";
        }
    }
}
