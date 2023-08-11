using Microsoft.AspNetCore.Mvc;

namespace TicTacToeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("GetAllGames")]
    public async Task<ActionResult<ServiceResponse<List<GetGameDto>>>> GetAllGames()
    {
        var result = _gameService.GetAllGames();
        return Ok(await result);
    }

    [HttpGet("GetGameById/{id}")]
    public async Task<ActionResult<ServiceResponse<GetGameDto>>> GetGameById(int id)
    {
        var result = _gameService.GetGameById(id);
        return Ok(await result);
    }

    [HttpPost("StartNewGame")]
    public async Task<ActionResult<ServiceResponse<GetGameDto>>> StartNewGame()
    {
        var newGame = new StartNewGameDto();
        var result = _gameService.StartNewGame(newGame);
        return Ok(await result);
    }

    [HttpPut("UpdateGame")]
    public async Task<ActionResult<ServiceResponse<GetGameDto>>> UpdateGame(UpdateGameDto updatedGame)
    {
        var result = _gameService.UpdateGame(updatedGame);
        return Ok(await result);
    }
}
