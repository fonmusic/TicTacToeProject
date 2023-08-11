import {TicTacToeGameState} from "./ticTacToeGameState";

export interface StartNewGameDto {
  board: string;
  nextPlayer: string;
  winner: string;
  gameState: TicTacToeGameState
}
