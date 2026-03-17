using Microsoft.EntityFrameworkCore;
using GameNLog.Data;
using GameNLog.DTOs;
using GameNLog.Models;

namespace GameNLog.Services
{
    public class GameService : IGameService
    {
        private readonly GameNLogContext _context;
        const string igdbURL = "https://images.igdb.com/igdb/image/upload/t_cover_big/";

        public GameService(GameNLogContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResultDTO<GameSummaryDTO>> GetGamesAsync(GameFilterDTO filter)
        {
            var query = _context.Games.AsQueryable();
            if (filter.Companies is not null)
            {
                query = query.Where(g => g.InvolvedCompanies.Any(ic => filter.Companies.Contains(ic.CompanyID)));
            }
            if (filter.Genres is not null)
            {
                query = query.Where(g => g.GameGenres.Any(gg => filter.Genres.Contains(gg.GenreID)));
            }
            if (filter.Platforms is not null)
            {
                query = query.Where(g => g.GamePlatforms.Any(gp => filter.Platforms.Contains(gp.PlatformID)));
            }

            if (filter.ReleaseDateFrom is not null)
            {
                query = query.Where(g => g.FirstReleaseDate >= filter.ReleaseDateFrom);
            }

            if (filter.ReleaseDateFrom is not null)
            {
                query = query.Where(g => g.FirstReleaseDate >= filter.ReleaseDateFrom);
            }

            if (filter.SearchString is not null)
            {
                query = query.Where(g => g.Name.Contains(filter.SearchString));
            }

            var totalCount = await query.CountAsync();
            var games = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(g => new GameSummaryDTO
                {
                    Id = g.GameID,
                    Name = g.Name,
                    CoverURL = igdbURL + g.Cover!.ImageID.ToString() + ".jpg",
                    AverageRating = g.PlayedGames
                        .SelectMany(pg => pg.Reviews)
                        .Average(r => r.Score)
                })
                .ToListAsync();

            return new PaginatedResultDTO<GameSummaryDTO>
            {
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize,
                Data = games
            };
        }

        public async Task<GameDetailDTO?> GetGameByIdAsync(int id)
        {
            return await _context.Games
                .Where(g => g.GameID == id)
                .Select(g => new GameDetailDTO
                {
                    Id = g.GameID,
                    Name = g.Name,
                    Summary = g.Summary,
                    CoverURL = igdbURL + g.Cover.ImageID.ToString() + ".jpg",
                    Genres = g.GameGenres
                        .Select(gg => gg.Genre.Name)
                        .ToList(),
                    Platforms = g.GamePlatforms
                        .Select(gp => gp.Platform.Name)
                        .ToList(),
                    Companies = g.InvolvedCompanies
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
