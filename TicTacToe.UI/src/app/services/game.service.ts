import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {UpdateGameDto} from "../models/updateGameDto";
import {Observable} from "rxjs";
import {ServiceResponse} from "../models/serviceResponse";
import {GetGameDto} from "../models/getGameDto";

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private apiUrl = `${environment.apiUrl}/Game`
  constructor(private http: HttpClient) { }

  getAllGames() : Observable<ServiceResponse<GetGameDto[]>> {
    return this.http.get<ServiceResponse<GetGameDto[]>>(`${this.apiUrl}/GetAllGames`);
  }

  getGameById(id: number) : Observable<ServiceResponse<GetGameDto>> {
    return this.http.get<ServiceResponse<GetGameDto>>(`${this.apiUrl}/GetGameById/${id}`);
  }

  startGame() : Observable<ServiceResponse<GetGameDto>> {
    return this.http.post<ServiceResponse<GetGameDto>>(`${this.apiUrl}/StartNewGame`, {});
  }

  updateGame(updateGameDto: UpdateGameDto) : Observable<ServiceResponse<GetGameDto>> {
    return this.http.put<ServiceResponse<GetGameDto>>(`${this.apiUrl}/UpdateGame`, updateGameDto);
  }
}
