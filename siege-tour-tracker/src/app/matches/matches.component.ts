import { Component, OnInit } from '@angular/core';
import { SeigeApiService } from './../services/seige-api.service';
import { IMatch, IDescription } from './../services/models';

//The matches component (primary component)
@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.scss']
})
export class MatchesComponent implements OnInit {
    matches: IMatch[] = [];
    descriptions: IDescription[] = [];
    leagues: string[] = [];

    status: number = 5;
    searchText: string = '';
    types: string[] = [];
    type: string = 'expanded';
    league: string = '';

    //Filter matches list
    get searchedMatches() {
        var matches : IMatch[] = [];

        for(let match of this.matches) {
            if (!this.filtermatch(match))
                continue;

            matches.push(match);
        }

        return matches;
    }

    constructor(
        private api: SeigeApiService
    ) {}

    //Initial our component
    async ngOnInit() {
        this.types = [ 'compact', 'compact-less', 'expanded' ];
        this.matches = await this.api.matchs();
        this.descriptions = await this.api.metaData();
        this.leagues = await this.api.leagues();
    }

    //Returns whether or not the match passes our filter criteria
    private filtermatch(match: IMatch) {
        if (match == null)
            return false;

        if (match.teamOne == null ||
            match.teamOne.fullName == null || match.teamOne.fullName == '' ||
            match.teamOne.name == null || match.teamOne.name == '')
            return false;

        if (match.teamTwo == null ||
            match.teamTwo.fullName == null ||
            match.teamTwo.fullName == '' ||
            match.teamTwo.name == null||
            match.teamTwo.name == '')
            return false; 

        if (match.league == null ||
            match.league.fullName == null ||
            match.league.name == null)
            return false;

        //Filter based on Match Status from the dropdown list
        if (match.status != this.status && this.status >= 0)
            return false;

        //Whether Match Status = Finished (TeamOneWon, TeamTwoWon, Draw)
        if (this.status == -2 && match.status != 1 && match.status != 2 && match.status != 3)
            return false;

        //Filter based on the league drop down
        if (this.league != "" && this.league != null) {
            var league = this.league.toLowerCase();
            if (match.league.name.toLowerCase().indexOf(league) == -1)
                return false;
        }

        //Return all that haven't failed our checks so far if search text is blank
        if (this.searchText == null ||
            this.searchText == "")
            return true;

        //Filter based on search text
        var search = this.searchText.toLowerCase();

        if (match.teamOne.fullName.toLowerCase().indexOf(search) == -1 &&
            match.teamOne.name.toLowerCase().indexOf(search) == -1 &&
            match.teamTwo.fullName.toLowerCase().indexOf(search) == -1 &&
            match.teamTwo.name.toLowerCase().indexOf(search) == -1 &&
            match.league.name.toLowerCase().indexOf(search) == -1 &&
            match.league.fullName.toLowerCase().indexOf(search) == -1)
            return false;

        return true;
    }
}
