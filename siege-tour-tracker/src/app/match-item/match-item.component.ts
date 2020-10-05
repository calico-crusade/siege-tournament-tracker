import { Component, Input } from '@angular/core';
import { IMatch } from '../services/models';

@Component({
    selector: 'app-match-item',
    templateUrl: './match-item.component.html',
    styleUrls: ['./match-item.component.scss']
})
export class MatchItemComponent {

    @Input()
    match: IMatch = null;

    @Input()
    type: ('compact' | 'compact-less' | 'expanded' | 'expanded-old' | 'super-compact') = 'compact';

    constructor() { }
}
