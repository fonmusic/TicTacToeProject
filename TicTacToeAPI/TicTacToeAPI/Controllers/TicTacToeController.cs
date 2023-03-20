using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Data;
using TicTacToeAPI.Models;

namespace TicTacToeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicTacToeController : ControllerBase
    {
        private readonly TicTacToeDbContext _dbContext;

        public TicTacToeController(TicTacToeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("StartNewGame")]
        public async Task<ActionResult<int>> StartNewGame()
        {
            var game = new TicTacToeGame();
            _dbContext.Add(game);
            await _dbContext.SaveChangesAsync();
            return game.Id;
        }

        [HttpGet("{id}/GetBoard")]
        public IActionResult GetBoard(int id)
        {
            var game = _dbContext.Find<TicTacToeGame>(id);
            if (game == null)
            {
                return NotFound();
            }
            var gameDto = game.ToDto();
            return Ok(gameDto);
        }

        [HttpPost("{id}/MakeMove")]
        public async Task<ActionResult<GameDto>> MakeMove(int id, MoveDto move)
        {
            var game = await _dbContext.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            if (!game.MakeMove(move.Row, move.Column))
            {
                return BadRequest("Invalid move");
            }

            await _dbContext.SaveChangesAsync();

            return game.ToDto();
        }

    }
}
