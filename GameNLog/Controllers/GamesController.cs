using GameNLog.DTOs;
using GameNLog.Services;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GameNLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameSummaryDTO>>> GetGames([FromQuery] GameFilterDTO filter)
        {
            var games = await _gameService.GetGamesAsync(filter);
            return Ok(games);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDetailDTO>> GetGame(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null)
                return NotFound();
            return Ok(game);
        }

    }
}
