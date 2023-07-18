namespace TicTacToeAPI.Dtos.Game;

public class StartNewGameDto
{
    public char[] Board { get; set; } = new char[9];
    public string NextPlayer { get; set; } = "X";
    public string Winner { get; set; } = string.Empty;
    public GameState GameState { get; set; } = GameState.XMove;
}
