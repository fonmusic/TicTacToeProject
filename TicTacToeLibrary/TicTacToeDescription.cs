namespace TicTacToeLibrary;

public class TicTacToeDescription
{
    public string Board { get; set; } = string.Empty;
    public string NextPlayer { get; set; } = string.Empty;
    public string Winner { get; set; } = string.Empty;
    public TicTacToeGameState GameState { get; set; }    
}
