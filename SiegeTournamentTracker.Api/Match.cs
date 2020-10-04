using System;

namespace SiegeTournamentTracker.Api
{
    public class Match
    {
        public LinkItem TeamOne { get; set; }
        public LinkItem TeamTwo { get; set; }
        public int? TeamOneScore { get; set; }
        public int? TeamTwoScore { get; set; }
        public DateTimeOffset Offset { get; set; }
        public LinkItem League { get; set; }
        public int BestOf { get; set; } = 1;

        public DateTime LocalTime => Offset.UtcDateTime.ToLocalTime();
        
        public MatchStatus Status
        {
            get 
            {
                if (TeamOneScore.HasValue && TeamTwoScore.HasValue)
                {
                    //Common bug with Liquidpedia
                    if (TeamOneScore == 0 && TeamTwoScore == 0)
                        return MatchStatus.Unknown;

                    if (TeamOneScore > TeamTwoScore)
                        return MatchStatus.TeamOneWon;
                    
                    if (TeamOneScore < TeamTwoScore)
                        return MatchStatus.TeamTwoWon;
                    
                    return MatchStatus.Draw;
                }

                var now = DateTime.Now;
                var local = LocalTime;
                if (local > now)
                    return MatchStatus.Upcoming;

                if (local <= now && local > now.AddMinutes(-120))
                    return MatchStatus.Active;

                return MatchStatus.Unknown;
            }
        }

        public override string ToString()
        {
            return $"{TeamOne}\r\nVS\r\n{TeamTwo}\r\n{Offset}\r\n{League}\r\n{TeamOneScore}:{TeamTwoScore}";
        }
    }
}
