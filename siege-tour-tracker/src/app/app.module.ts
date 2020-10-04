import { BrowserModule } from '@angular/platform-browser'; 
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MatchesComponent } from './matches/matches.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { MatchItemComponent } from './match-item/match-item.component';
import { LiqImgUrlPipe } from './services/liq-img-url.pipe';
import { DecodeHtmlPipe } from './services/decode-html.pipe';

@NgModule({
  declarations: [
    AppComponent,
    MatchesComponent,
    NotFoundComponent,
    MatchItemComponent,
    LiqImgUrlPipe,
    DecodeHtmlPipe
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
