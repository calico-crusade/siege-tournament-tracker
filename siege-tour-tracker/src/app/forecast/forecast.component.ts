import { Component, OnInit } from '@angular/core';
import { IMatch, IMatchDay } from '../services/models';
import { SeigeApiService } from '../services/seige-api.service';

@Component({
    selector: 'app-forecast',
    templateUrl: './forecast.component.html',
    styleUrls: ['./forecast.component.scss']
})
export class ForecastComponent implements OnInit {

    matchdays: IMatchDay[] = [];
    selected: number = 0;

    constructor(
        private api: SeigeApiService
    ) { }

    async ngOnInit() {
        var matches = await this.api.matchDays();

        this.matchdays = this.getFutureEvents(matches);
    }

    getFutureEvents(matches: IMatchDay[]) {
        var futureMatches = [];

        for(let match of matches) {
            if (match.offset > 0)
                continue;
            
            futureMatches.push(match);
        }

        return futureMatches;
    }
}
