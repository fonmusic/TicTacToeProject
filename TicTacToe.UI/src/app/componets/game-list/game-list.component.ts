import { Component } from '@angular/core';
import {GameService} from "../../services/game.service";
import {GetGameDto} from "../../models/getGameDto";

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css']
})
export class GameListComponent {
  constructor(private gameService: GameService) { }

  games: GetGameDto[] = [];
  selectedGame: GetGameDto | undefined;

  ngOnInit(): void {
    this.getAllGames();
  }

  getAllGames() {
    this.gameService.getAllGames().subscribe(response => {
      if (response.success) {
        this.games = response.data;
      }
    });
  }

  gameStateToString(gameState: number): string {
    switch (gameState) {
      case 0:
        return 'X Move';
      case 1:
        return 'O Move';
      case 2:
        return 'X Win';
      case 3:
        return 'O Win';
      case 4:
        return 'Draw';
      default:
        return 'Unknown';
    }
  }

openGameBoard(game: GetGameDto) {
    this.selectedGame = game;
  }

}
