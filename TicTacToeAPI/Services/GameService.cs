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

        var ticTacToeGame = new TicTacToe
        {
            Description = new TicTacToeDescription
            {
                Board = game.Board,
                NextPlayer = game.NextPlayer,
                Winner = game.Winner,
                GameState = game.GameState
            }
        };

        if (!ticTacToeGame.MakeMove(updatedGame.Position))
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"Invalid move.";
            return serviceResponse;
        }

        game.Board = ticTacToeGame.Description.Board;
        game.NextPlayer = ticTacToeGame.Description.NextPlayer;
        game.Winner = ticTacToeGame.Description.Winner;
        game.GameState = ticTacToeGame.Description.GameState;

        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetGameDto>(game);
        if (ticTacToeGame.Description.Winner != string.Empty)
            serviceResponse.Message = $"Game over. {ticTacToeGame.Description.Winner} wins!";
        else if (ticTacToeGame.Description.GameState == TicTacToeGameState.Draw)
            serviceResponse.Message = $"Game over. Draw!";
        else if (ticTacToeGame.Description.GameState == TicTacToeGameState.XMove)
            serviceResponse.Message = $"X's move.";
        else if (ticTacToeGame.Description.GameState == TicTacToeGameState.OMove)
            serviceResponse.Message = $"O's move.";
        else
            serviceResponse.Message = $"Game updated.";

        return serviceResponse;
    }
}
