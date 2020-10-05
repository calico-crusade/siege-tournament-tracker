import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { SeigeApiService } from '../services/seige-api.service';
import { IMatch } from '../services/models';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit {

    days: IDay[] = [];
    matches: IMatch[] = [];

    index: number = 0;

    constructor(
        private api: SeigeApiService
    ) { }

    async ngOnInit() {
        this.matches = await this.api.matchs();
        this.genDays();

        this.startDate();
    }

    genDays() {

        this.days = [];
        let entityCount = 7 * 5;

        let startDate = this.startDate();
        let curDate = moment(new Date());
        for(let i = 0; i < entityCount; i++) {

            let date = moment(startDate).add(i, 'd');
            let offset = curDate.diff(date, 'd');
            if (offset <= 0 && date.toDate().toDateString() != curDate.toDate().toDateString())
                offset -= 1;

            let day: IDay = {
                date: date.toDate(),
                curDateOffset: offset,
                events: this.genEvents(date.toDate())
            }
            this.days.push(day);
        }

    }

    getClassName(day: IDay) {
        if (day.curDateOffset == -1)
            return 'day-next';
        if (day.curDateOffset == 0)
            return 'day-current';
        if (day.curDateOffset == 1)
            return 'day-previous';

        return '';
    }

    genEvents(date: Date) {
        
        let events:IMatch[] = [];

        for(let evt of this.matches){
            var offset = moment(evt.offset);

            if (date.getDate() != offset.date() ||
                date.getFullYear() != offset.year() ||
                date.getMonth() != offset.month())
                continue;

            events.push(evt);
        }

        return events;

        /*let eventCount = Math.floor(Math.random() * 10);
        let events:IMatch[] = [];

        for(let i = this.index; i < eventCount && i < this.matches.length; i++) {
            events.push(this.matches[i]);
        }

        return events;*/
    }

    startDate(date?: Date) {
        date = date || new Date();
        var curDate = moment(date);
        var firstOfMonth = moment(new Date(curDate.year(), curDate.month(), 1));
        var firstDayOfWeek = firstOfMonth.weekday();
        return firstOfMonth.subtract(firstDayOfWeek - 1, 'd').toDate();
    }

}

export interface IDay {
    date: Date;
    curDateOffset: number;
    events: IMatch[];
}
