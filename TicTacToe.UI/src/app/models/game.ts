export class Game {
    id?: number;
    board = '';
    nextPlayer = '';
    winner = '';
    gameState?: GameState;
}

export enum GameState {
    XMove = 'XMove',
    OMove = 'OMove',
    XWin = 'XWin',
    OWin = 'OWin',
    Draw = 'Draw'
}