namespace TicTacToeAPI.Models;
public class Game
{
    public int Id { get; set; }
    public required string Board { get; set; }
    public string NextPlayer { get; set; } = string.Empty;
    public string Winner { get; set; } = string.Empty;
    public TicTacToeGameState GameState { get; set; }
}
