import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IMatch, IDescription, IMatchDay } from './models';
import * as moment from 'moment';
import * as _ from 'underscore';

//Service for connecting to the API
@Injectable({
    providedIn: 'root'
})
export class SeigeApiService {

    private baseUrl = '/api/';

    constructor(
        private http: HttpClient
    ) { }

    matchs() {
        return this.get<IMatch[]>('matches/all');
    }

    upcoming() {
        return this.get<IMatch[]>('matches/upcoming');
    }

    metaData() {
        return this.get<IDescription[]>('metadata/matchstatus');
    }

    leagues() {
        return this.get<string[]>('metadata/leagues');
    }

    async matchDays() {
        const matches = await this.matchs();
        let days: IMatchDay[] = [];

        const min:IMatch = <any>_.min(matches, (m) => moment(m.offset).unix());
        const max:IMatch = <any>_.max(matches, (m) => moment(m.offset).unix());
        const startDate = moment(min.offset).startOf('day');
        const endDate = moment(max.offset).startOf('day');
        const dayCount = endDate.diff(startDate, 'd') + 1;

        for(let i = 0; i < dayCount; i++) {
            const date = moment(startDate).add(i, 'd');

            days.push({
                date: date.toDate(),
                offset: this.getOffset(date.toDate()),
                matches: _.filter(matches, t => moment(t.offset).date() == date.date())
            });
        }
        
        return days;
    }

    private getOffset(date: Date) {
        const curDate = moment(new Date()).startOf('day');
        const momDate = moment(date).startOf('day');

        return curDate.diff(momDate, 'd');
    }

    private get<T>(url: string) {
        if (url.indexOf('://') === -1)
            url = this.baseUrl + url;

        return this.http.get<T>(url).toPromise();
    }
}
