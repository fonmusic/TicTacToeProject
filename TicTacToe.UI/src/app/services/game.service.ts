import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from '../models/game';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private API_URL = 'https://localhost:5121/api/Game';

  constructor(private http: HttpClient) { }

  getAllGames() : Observable<Game[]> {
    return this.http.get<Game[]>(`${this.API_URL}/GetAllGames`);
  } 

  getGameById(id: number) : Observable<Game> {
    return this.http.get<Game>(`${this.API_URL}/GetGameById/${id}`);
  }

  startNewGame() : Observable<Game> {
    return this.http.post<Game>(`${this.API_URL}/StartNewGame`, null);
  }

  updateGame(gameId: number, position: number) : Observable<Game> {
    return this.http.post<Game>(`${this.API_URL}/UpdateGame`, { id: gameId, position });
  }
    
}
