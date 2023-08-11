namespace TicTacToeApp.Services;

public interface IGameService
{
    Task<ServiceResponse<List<GetGameDto>>> GetAllGames();
    Task<ServiceResponse<GetGameDto>> GetGameById(int id);
    Task<ServiceResponse<GetGameDto>> StartNewGame(StartNewGameDto newGame);
    Task<ServiceResponse<GetGameDto>> UpdateGame(UpdateGameDto updatedGame);
}