namespace TicTacToeApp.Dtos;

public class GetGameDto
{
    public int Id { get; set; }
    public TicTacToeDescription Description { get; set; } = new();
}