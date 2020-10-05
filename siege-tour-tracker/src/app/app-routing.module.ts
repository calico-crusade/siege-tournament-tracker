import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CalendarComponent } from './calendar/calendar.component';
import { ForecastComponent } from './forecast/forecast.component';
import { MatchesComponent } from './matches/matches.component';
import { NotFoundComponent } from './not-found/not-found.component';

//The apps client-side routes
const routes: Routes = [
    //Default route - redirects to /matches
    {
        path: '',
        redirectTo: '/matches',
        pathMatch: 'full'
    },
    //Matches route 
    {
        path: 'matches',
        component: MatchesComponent
    }, 
    //Calendar route
    {
        path: 'calendar',
        component: CalendarComponent
    },
    {
        path: 'forecast',
        component: ForecastComponent
    },
    //Every other route
    {
        path: '**',
        component: NotFoundComponent
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
