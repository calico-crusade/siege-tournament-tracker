namespace SiegeTournamentTracker.Api
{
    public enum MatchStatus
    {
        /// <summary>
        /// Unable to determine the matches status
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Team #1 beat team #2
        /// </summary>
        TeamOneWon = 1,
        /// <summary>
        /// Team #2 beat team #1
        /// </summary>
        TeamTwoWon = 2,
        /// <summary>
        /// The result was a draw
        /// </summary>
        Draw = 3,
        /// <summary>
        /// The game is currently on-going
        /// </summary>
        Active = 4,
        /// <summary>
        /// The game has yet to happen
        /// </summary>
        Upcoming = 5
    }
}
