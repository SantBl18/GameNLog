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
                query = query.Where(g => EF.Functions.ToTsVector("english", g.Name)
                    .Matches(EF.Functions.PlainToTsQuery("english", filter.SearchString))
                    || EF.Functions.ILike(g.Name, $"%{filter.SearchString}%"));
            }

            if (filter.SortBy is not null)
            {
                switch (filter.SortBy)
                {
                    case "name":
                        query = filter.SortDesc ? query.OrderByDescending(g => g.Name) :
                            query.OrderBy(g => g.Name);
                        break;

                    case "date":
                        query = filter.SortDesc ? query.OrderByDescending(g => g.FirstReleaseDate) :
                            query.OrderBy(g => g.FirstReleaseDate);
                        break;

                    case "rating":
                        query = filter.SortDesc ? query.OrderByDescending(g => g.PlayedGames
                        .SelectMany(pg => pg.Reviews)
                        .Average(r => r.Score)) :
                        query.OrderBy(g => g.PlayedGames
                        .SelectMany(pg => pg.Reviews)
                        .Average(r => r.Score));
                        break;
                }
            }

            var totalCount = await query.CountAsync();
            var games = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(g => new GameSummaryDTO
                {
                    Id = g.Id,
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
            var recentReviews = await _context.PlayedGameReviews
                .Where(r => r.PlayedGame.GameID == id)
                .OrderByDescending(r => r.ReviewedAt)
                .Take(10)
                .Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    UserId = r.PlayedGame.User.Id,
                    Username = r.PlayedGame.User.Username,
                    Rating = r.Score,
                    Description = r.Description,
                    CreatedAt = r.ReviewedAt
                })
                .ToListAsync();

            return await _context.Games
                .Where(g => g.Id == id)
                .Select(g => new GameDetailDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                    Summary = g.Summary,
                    CoverURL = igdbURL + g.Cover!.ImageID.ToString() + ".jpg",
                    Genres = g.GameGenres
                        .Select(gg => gg.Genre!.Name)
                        .ToList(),
                    Platforms = g.GamePlatforms
                        .Select(gp => gp.Platform!.Name)
                        .ToList(),
                    Companies = g.InvolvedCompanies
                        .Select(gc => gc.Company!.Name)
                        .ToList(),
                    AverageRating = g.PlayedGames
                        .SelectMany(pg => pg.Reviews)
                        .Average(r => (double?)r.Score),
                    ReviewCount = g.PlayedGames
                        .SelectMany(pg => pg.Reviews)
                        .Count(),
                    RecentReviews = recentReviews

                })
                .FirstOrDefaultAsync();
        }
    }
}
