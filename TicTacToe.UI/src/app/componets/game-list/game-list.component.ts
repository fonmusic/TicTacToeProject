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
}
