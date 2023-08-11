import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {UpdateGameDto} from "../models/updateGameDto";

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private apiUrl = `${environment.apiUrl}/Game`
  constructor(private http: HttpClient) { }

  getAllGames() {
    return this.http.get(`${this.apiUrl}/GetAllGames`);
  }

  getGameById(id: number) {
    return this.http.get(`${this.apiUrl}/GetGameById/${id}`);
  }

  startNewGame() {
    return this.http.post(`${this.apiUrl}/StartNewGame`, null);
  }

  updateGame(updatedGame: UpdateGameDto) {
    return this.http.put(`${this.apiUrl}/UpdateGame`, updatedGame);
  }
}
