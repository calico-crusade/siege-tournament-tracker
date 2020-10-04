import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IMatch, IDescription } from './models';

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

    private get<T>(url: string) {
        if (url.indexOf('://') === -1)
            url = this.baseUrl + url;

        return this.http.get<T>(url).toPromise();
    }
}
