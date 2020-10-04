import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
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
