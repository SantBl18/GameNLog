using GameNLog.DTOs;
using GameNLog.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameNLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameSummaryDTO>>> GetGames()
        {
            var games = await _gameService.GetGamesAsync();
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
