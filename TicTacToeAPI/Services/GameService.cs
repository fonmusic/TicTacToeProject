namespace TicTacToeAPI.Services;

public class GameService : IGameService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public GameService(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetGameDto>>> GetAllGames()
    {
        var games = await _context.Games
            .Include(g => g.TicTacToe)
            .ThenInclude(t => t.Description)
            .ToListAsync();
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
        var game = await _context.Games
            .Include(g => g.TicTacToe)
            .ThenInclude(t => t.Description)
            .FirstOrDefaultAsync(g => g.Id == id);

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
        game.TicTacToe = new TicTacToe
        {
            Description = new TicTacToeDescription
            {   
                Board = newGame.Board,
                NextPlayer = newGame.NextPlayer,
                GameState = newGame.GameState,
                Winner = newGame.Winner
            }
        };
        
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
        
        serviceResponse.Data = _mapper.Map<GetGameDto>(game);
        serviceResponse.Message = $"New game started.";
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetGameDto>> UpdateGame(UpdateGameDto updatedGame)
    {
        var serviceResponse = new ServiceResponse<GetGameDto>();
        var game = await _context.Games
            .Include(g => g.TicTacToe) 
            .ThenInclude(t => t.Description)
            .FirstOrDefaultAsync(g => g.Id == updatedGame.Id);
        if (game is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"Game with id {updatedGame.Id} not found.";
            return serviceResponse;
        }

        if (!game.TicTacToe.MakeMove(updatedGame.Position))
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"Invalid move.";
            return serviceResponse;
        }

        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetGameDto>(game);
        if (game.TicTacToe.Description.Winner != string.Empty)
            serviceResponse.Message = $"Game over. {game.TicTacToe.Description.Winner} wins!";
        else if (game.TicTacToe.Description.GameState == TicTacToeGameState.Draw)
            serviceResponse.Message = $"Game over. Draw!";
        else if (game.TicTacToe.Description.GameState == TicTacToeGameState.XMove)
            serviceResponse.Message = $"X's move.";
        else if (game.TicTacToe.Description.GameState == TicTacToeGameState.OMove)
            serviceResponse.Message = $"O's move.";
        else
            serviceResponse.Message = $"Game updated.";

        return serviceResponse;
    }
}
