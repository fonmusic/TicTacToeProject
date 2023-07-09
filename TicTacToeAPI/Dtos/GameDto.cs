namespace TicTacToeAPI.Dtos;

public class GameDto
{
    public char[]? Cells { get; set; }
    public GameState State { get; set; }
    public int Id { get; set; }
}

public enum GameState
{
    XMove,
    OMove,
    XWin,
    OWin,
    Draw
};
