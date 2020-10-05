import { IMatch } from './match.model';

export interface IMatchDay {
    date: Date;
    offset: number;
    matches: IMatch[];
}