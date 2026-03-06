using Microsoft.EntityFrameworkCore;
using GameNLog.Data;
using GameNLog.DTOs;
using GameNLog.Models;

namespace GameNLog.Services
{
    public class GameService : IGameService
    {
        private readonly GameNLogContext _context;
        const string igdbURL = "https://images.igdb.com/igdb/image/upload/t_cover_big";

        public GameService(GameNLogContext context)
        {
            _context = context;
        }

        public async Task<List<GameSummaryDTO>> GetGamesAsync()
        {
            
            return await _context.Games
                .Select(g => new GameSummaryDTO
                {
                    Id = g.GameID,
                    Name = g.Name,
                    CoverURL = igdbURL + g.GameCover.ImageID.ToString() + ".jpg",
                    AverageRating = g.PlayedGames
                        .SelectMany(pg => pg.Reviews)
                        .Average(r => r.Score)
                })
                .ToListAsync();
        }

        public async Task<GameDetailDTO?> GetGameByIdAsync(int id)
        {
            return await _context.Games
                .Where(g => g.GameID == id)
                .Select(g => new GameDetailDTO
                {
                    Id = g.GameID,
                    Name = g.Name,
                    CoverURL = igdbURL + g.GameCover.ImageID.ToString() + ".jpg",
                    Genres = g.GameGenres
                        .Select(gg => gg.Genre.Name)
                        .ToList(),
                    Platforms = g.GamePlatforms
                        .Select(gp => gp.Platform.Name)
                        .ToList(),
                    Companies = g.GameCompanies
                        .Select(gc => gc.Company.Name)
                        .ToList(),
                    AverageRating = g.PlayedGames
                        .SelectMany(pg => pg.Reviews)
                        .Average(r => (double?)r.Score),
                    ReviewCount = g.PlayedGames
                        .SelectMany(pg => pg.Reviews)
                        .Count()

                })
                .FirstOrDefaultAsync();
        }
    }
}
