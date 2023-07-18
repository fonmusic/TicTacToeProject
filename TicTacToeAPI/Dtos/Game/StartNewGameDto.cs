namespace TicTacToeAPI.Dtos.Game;

public class StartNewGameDto
{
    private const string EmptyBoard = "         "; // 9 spaces
    public string Board { get; set; } = EmptyBoard;
    public string NextPlayer { get; set; } = "X";
    public string Winner { get; set; } = string.Empty;
    public GameState GameState { get; set; } = GameState.XMove;
}
