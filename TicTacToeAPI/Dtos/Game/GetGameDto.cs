namespace TicTacToeAPI.Dtos.Game;

public class GetGameDto
{
    public int Id { get; set; }
    public TicTacToeDescription Description { get; set; } = new();
}
