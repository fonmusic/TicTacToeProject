import { Component, OnInit } from '@angular/core';
import { Game } from 'src/app/models/game';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css']
})
export class GameBoardComponent implements OnInit {
  game: Game = new Game();
  currentPlayer: string = '';
  isGameOver = false;

  constructor(private gameService: GameService) { }

  ngOnInit(): void {
  }

  startNewGame(): void {
    this.gameService.startNewGame().subscribe(game => {
      this.game = game;
      this.currentPlayer = game.nextPlayer;
      this.isGameOver = false;
    });
  }

  // makeMove(position: number): void {
  //   this.gameService.updateGame(this.game.id, position).subscribe(game => {
  //     this.game = game;
  //     this.currentPlayer = game.nextPlayer;
  //     this.isGameOver = game.gameState !== undefined;
  //   });
  // }
}
