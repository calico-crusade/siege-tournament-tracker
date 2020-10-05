using System;

namespace SiegeTournamentTracker.Api
{
    /// <summary>
    /// Represents a R6 match
    /// </summary>
    public class Match
    {
        /// <summary>
        /// The first teams details
        /// </summary>
        public LinkItem TeamOne { get; set; }

        /// <summary>
        /// The second teams details
        /// </summary>
        public LinkItem TeamTwo { get; set; }

        /// <summary>
        /// The first teams score, or null if they don't have one yet
        /// </summary>
        public int? TeamOneScore { get; set; }

        /// <summary>
        /// The second teams score, or null if they don't have one yet
        /// </summary>
        public int? TeamTwoScore { get; set; }

        /// <summary>
        /// The UTC date time offset for the matches start time
        /// </summary>
        public DateTimeOffset Offset { get; set; }

        /// <summary>
        /// The leagues details
        /// </summary>
        public LinkItem League { get; set; }

        /// <summary>
        /// The number of maps / games in this match
        /// </summary>
        public int BestOf { get; set; } = 1;

        /// <summary>
        /// The server's local time for this event
        /// </summary>
        public DateTime Timestamp => Offset.DateTime;
        
        /// <summary>
        /// The server's best guess at the status of the match
        /// </summary>
        public MatchStatus Status
        {
            get 
            {
                if (TeamOneScore.HasValue && TeamTwoScore.HasValue &&
                    TeamOneScore != 0 && TeamTwoScore != 0)
                {
                    if (TeamOneScore > TeamTwoScore)
                        return MatchStatus.TeamOneWon;
                    
                    if (TeamOneScore < TeamTwoScore)
                        return MatchStatus.TeamTwoWon;
                    
                    return MatchStatus.Draw;
                }

                var now = DateTime.Now;
                var local = Timestamp;
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
