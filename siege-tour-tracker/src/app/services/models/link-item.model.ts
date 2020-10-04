//Represents a link item (see SiegeTournamentTracker.Api/LinkItem.cs)
export interface ILinkItem {
    fullName: null | string;
    name:     string;
    url:      null | string;
    imageUrl: null | string;
}