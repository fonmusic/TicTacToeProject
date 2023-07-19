import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from '../models/game';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private apiUrl = `${environment.apiUrl}/Game`;

  constructor(private http: HttpClient) { }

    getAllGames(): Observable<Game[]> {
      return this.http.get<Game[]>(`${this.apiUrl}/GetAllGames`);
    }

    getGameById(id: number): Observable<Game> {
      return this.http.get<Game>(`${this.apiUrl}/GetGameById/${id}`);
    }

    startNewGame(): Observable<Game> {
      return this.http.get<Game>(`${this.apiUrl}/StartNewGame`);
    }

    updateGame(id: number, position: number): Observable<Game> {
      return this.http.get<Game>(`${this.apiUrl}/UpdateGame`);
    }
      
    
}
