namespace TicTacToeAPI.Dtos.Game;

public class GetGameDto
{
    public int Id { get; set; }
    public TicTacToeDescription Description { get; set; } = new();
    // public required string Board { get; set; }
    // public string NextPlayer { get; set; } = string.Empty;
    // public string Winner { get; set; } = string.Empty;
    // public TicTacToeGameState GameState { get; set; }
}
