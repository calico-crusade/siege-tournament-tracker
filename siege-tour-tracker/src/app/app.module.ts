import { BrowserModule } from '@angular/platform-browser'; 
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

//Our components and other things that need to be declared / imported
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MatchesComponent } from './matches/matches.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { MatchItemComponent } from './match-item/match-item.component';
import { LiqImgUrlPipe } from './services/liq-img-url.pipe';
import { DecodeHtmlPipe } from './services/decode-html.pipe';
import { CalendarComponent } from './calendar/calendar.component';
import { ForecastComponent } from './forecast/forecast.component';

//Root module for the entire application, all imports and declarations can go in here since the app isn't using lazy-loading for routing.
@NgModule({
  declarations: [
    AppComponent,
    MatchesComponent,
    NotFoundComponent,
    MatchItemComponent,
    LiqImgUrlPipe,
    DecodeHtmlPipe,
    CalendarComponent,
    ForecastComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
