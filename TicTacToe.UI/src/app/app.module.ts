import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GameBoardComponent } from './componets/game-board/game-board.component';
import { GameListComponent } from './componets/game-list/game-list.component';
import { GameDetailsComponent } from './componets/game-details/game-details.component';

@NgModule({
  declarations: [
    AppComponent,
    GameBoardComponent,
    GameListComponent,
    GameDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
