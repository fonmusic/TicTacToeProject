import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TicTacToe.UI';

  constructor() {
    console.log('App Component');
  }

  showBoard = false;
  buttonBoardLabel = 'Start Game';

  showListOfGames = false;
  buttonListOfGamesLabel = 'Show List of Games';

  toggleBoard() {
    this.showBoard = !this.showBoard;
    this.buttonBoardLabel = this.showBoard ? 'Exit Game' : 'Start Game';
  }

  toggleListOfGames() {
    this.showListOfGames = !this.showListOfGames;
    this.buttonListOfGamesLabel = this.showListOfGames ? 'Hide List of Games' : 'Show List of Games';
  }
}
