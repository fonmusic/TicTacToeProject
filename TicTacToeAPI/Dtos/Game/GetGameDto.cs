namespace TicTacToeAPI.Dtos.Game;

public class GetGameDto
{
    public int Id { get; set; }
    public required char[] Board { get; set; }
    public string NextPlayer { get; set; } = string.Empty;
    public string Winner { get; set; } = string.Empty;
    public GameState GameState { get; set; }
}