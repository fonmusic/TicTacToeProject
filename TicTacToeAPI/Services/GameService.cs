namespace TicTacToeAPI.Services;

public class GameService : IGameService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly ITicTacToe _ticTacToe;
    
    private const int MinPosition = 0;
    private const int MaxPosition = 8;

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
        
        if (!_ticTacToe.MakeMove(updatedGame.Position, ticTacToeDescription) 
            || updatedGame.Position < MinPosition
            || updatedGame.Position > MaxPosition)
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
        serviceResponse.Message = game.GameState switch
        {
            TicTacToeGameState.Draw => $"Game with id {updatedGame.Id} ended in a draw.",
            TicTacToeGameState.XWin => $"Game with id {updatedGame.Id} ended with X as the winner.",
            TicTacToeGameState.OWin => $"Game with id {updatedGame.Id} ended with O as the winner.",
            TicTacToeGameState.XMove => $"Game with id {updatedGame.Id} updated. X to move.",
            TicTacToeGameState.OMove => $"Game with id {updatedGame.Id} updated. O to move.",
            _ => throw new Exception("Invalid game state.")
        };
    }
}