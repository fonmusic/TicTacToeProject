namespace TicTacToeAPI.Services;

public class GameService : IGameService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly ITicTacToe _ticTacToe;

    public GameService(IMapper mapper, DataContext context, ITicTacToe ticTacToe)
    {
        _context = context;
        _mapper = mapper;
        _ticTacToe = ticTacToe;
    }

    public async Task<ServiceResponse<List<GetGameDto>>> GetAllGames()
    {
        var games = await _context.Games.ToListAsync();
        var serviceResponse = new ServiceResponse<List<GetGameDto>>
        {
            Data = _mapper.Map<List<GetGameDto>>(games),
            Message = "All games found."
        };
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetGameDto>> GetGameById(int id)
    {
        var serviceResponse = new ServiceResponse<GetGameDto>();
        var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

        if (game is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"Game with id {id} not found.";
            return serviceResponse;
        }

        serviceResponse.Data = _mapper.Map<GetGameDto>(game);
        serviceResponse.Message = $"Game with id {id} found.";
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetGameDto>> StartNewGame(StartNewGameDto newGame)
    {
        var serviceResponse = new ServiceResponse<GetGameDto>();

        var game = _mapper.Map<Game>(newGame);

        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetGameDto>(game);
        serviceResponse.Message = $"New game started.";
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetGameDto>> UpdateGame(UpdateGameDto updatedGame)
    {
        var serviceResponse = new ServiceResponse<GetGameDto>();
        var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == updatedGame.Id);
        if (game is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"Game with id {updatedGame.Id} not found.";
            return serviceResponse;
        }
        
        var ticTacToeDescription = _mapper.Map<TicTacToeDescription>(game);
        
        if (!_ticTacToe.MakeMove(updatedGame.Position, ticTacToeDescription))
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"Invalid move.";
            return serviceResponse;
        }
        
        _mapper.Map(ticTacToeDescription, game);
        
        await _context.SaveChangesAsync();
        
        serviceResponse.Data = _mapper.Map<GetGameDto>(game);
        PrintUpdateMessage(updatedGame, game, serviceResponse);
        return serviceResponse;
    }

    private static void PrintUpdateMessage(UpdateGameDto updatedGame, Game game, ServiceResponse<GetGameDto> serviceResponse)
    {
        if (game.GameState == TicTacToeGameState.Draw)
            serviceResponse.Message = $"Game with id {updatedGame.Id} ended in a draw.";
        else if (game.GameState == TicTacToeGameState.XWin)
            serviceResponse.Message = $"Game with id {updatedGame.Id} ended with X as the winner.";
        else if (game.GameState == TicTacToeGameState.OWin)
            serviceResponse.Message = $"Game with id {updatedGame.Id} ended with O as the winner.";
        else if (game.GameState == TicTacToeGameState.XMove)
            serviceResponse.Message = $"Game with id {updatedGame.Id} updated. X to move.";
        else if (game.GameState == TicTacToeGameState.OMove)
            serviceResponse.Message = $"Game with id {updatedGame.Id} updated. O to move.";
        else
            throw new Exception("Invalid game state.");
    }
}