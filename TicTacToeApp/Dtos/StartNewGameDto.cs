namespace TicTacToeApp.Dtos;

public class StartNewGameDto
{
    private const string DefaultBoard = "         ";
    public string Board { get; set; } = DefaultBoard;
    public string NextPlayer { get; set; } = "X";
    public string Winner { get; set; } = string.Empty;
    public TicTacToeGameState GameState { get; set; } = TicTacToeGameState.XMove;
}