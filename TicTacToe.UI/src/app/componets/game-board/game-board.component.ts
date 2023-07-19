import { Component, OnInit } from '@angular/core';
import { Game, GameState } from 'src/app/models/game';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css']
})
export class GameBoardComponent implements OnInit {

  game: Game = new Game;
  isGameOver = false;

  constructor(private gameService: GameService) { }

  ngOnInit(): void {
    this.startNewGame();
  }

  startNewGame(): void {
    this.gameService.startNewGame().subscribe(game => {
      this.game = game;
    });
  }

  makeMove(position: number): void {
    this.gameService.updateGame(this.game.id, position).subscribe(game => {
      this.game = game;
    });
  }

  private checkGameOver(): void {
    if (this.game.gameState === GameState.Draw || this.game.winner !== '') {
      this.isGameOver = true;
    }
  }
  
}
