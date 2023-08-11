namespace TicTacToeAPI.Models;
public class Game
{
    public int Id { get; set; }
    public required string Board { get; set; }
    public required string NextPlayer { get; set; }
    public required string Winner { get; set; }
    public TicTacToeGameState GameState { get; set; }
}
