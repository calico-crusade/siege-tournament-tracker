import { ILinkItem } from './link-item.model';

//Represents the match object (see SiegeTournamentTracker.Api/Match.cs)
export interface IMatch {
    teamOne:      ILinkItem;
    teamTwo:      ILinkItem;
    teamOneScore: number | null;
    teamTwoScore: number | null;
    offset:       Date;
    league:       ILinkItem;
    bestOf:       number;
    localTime:    Date;
    status:       number;
}