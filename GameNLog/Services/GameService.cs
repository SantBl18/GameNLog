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

            if (filter.Genres.Any())
            {
                query = query
                    .Where(g => g.GameGenres.Any(gg => filter.Genres.Contains(gg.GenreId)));
            }
            if (filter.Platforms.Any())
            {
                query = query
                    .Where(g => g.GamePlatforms.Any(gp => filter.Platforms.Contains(gp.PlatformId)));
            }

            if (filter.ReleaseDateFrom is not null)
            {
                query = query
                    .Where(g => g.FirstReleaseDate >= filter.ReleaseDateFrom);
            }

            if (filter.ReleaseDateTo is not null)
            {
                query = query
                    .Where(g => g.FirstReleaseDate <= filter.ReleaseDateTo);
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
                    case "date":
                        query = filter.SortDesc
                            ? query.OrderByDescending(g => g.FirstReleaseDate).ThenByDescending(g => g.Id)
                            : query.OrderBy(g => g.FirstReleaseDate).ThenBy(g => g.Id);
                        break;

                    case "rating":
                        query = filter.SortDesc 
                            ? query.OrderByDescending(g => g.BayesianRating).ThenByDescending(g => g.Id)
                            : query.OrderBy(g => g.BayesianRating).ThenBy(g => g.Id);
                        break;

                    default:
                        query = filter.SortDesc 
                            ? query.OrderByDescending(g => g.Name).ThenByDescending(g => g.Id)
                            : query.OrderBy(g => g.Name).ThenBy(g => g.Id);
                        break;
                }
            }

            if (filter.SortBy == "date" && filter.LastDate is not null && filter.LastId is not null)
            {
                query = filter.SortDesc 
                    ? query.Where(g => g.FirstReleaseDate < filter.LastDate ||
                    (g.FirstReleaseDate == filter.LastDate && g.Id < filter.LastId))
                    : query.Where(g => g.FirstReleaseDate > filter.LastDate ||
                    (g.FirstReleaseDate == filter.LastDate && g.Id > filter.LastId));
            }

            else if (filter.SortBy == "rating" && filter.LastRating is not null && filter.LastId is not null)
            {
                query = filter.SortDesc
                    ? query.Where(g => g.BayesianRating < filter.LastRating ||
                    (g.BayesianRating == filter.LastRating && g.Id < filter.LastId))
                    : query.Where(g => g.BayesianRating > filter.LastRating ||
                    (g.BayesianRating == filter.LastRating && g.Id > filter.LastId));
            }

            var games = await query
                .Take(filter.PageSize)
                .Select(g => new GameSummaryDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                    CoverURL = g.Cover != null
                        ? igdbURL + g.Cover.ImageId + ".jpg"
                        : null,
                    AverageRating = g.AverageRating
                })
                .ToListAsync();

            return new PaginatedResultDTO<GameSummaryDTO>
            {
                PageSize = filter.PageSize,
                Data = games
            };
        }

        public async Task<GameDetailDTO?> GetGameByIdAsync(int id)
        {
            var recentReviews = await _context.GameReviews
                .Where(r => r.GameLog.GameId == id)
                .OrderByDescending(r => r.ReviewedAt)
                .Take(10)
                .Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    UserId = r.GameLog.User.Id,
                    Username = r.GameLog.User.Username,
                    Rating = r.GameLog.Score,
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
                    CoverURL = g.Cover != null
                        ? igdbURL + g.Cover.ImageId + ".jpg"
                        : null,
                    Genres = g.GameGenres
                        .Select(gg => gg.Genre!.Name)
                        .ToList(),
                    Platforms = g.GamePlatforms
                        .Select(gp => gp.Platform!.Name)
                        .ToList(),
                    Companies = g.InvolvedCompanies
                        .Select(gc => gc.Company!.Name)
                        .ToList(),
                    AverageRating = g.AverageRating,
                    ReviewCount = g.RatingCount,
                    RecentReviews = recentReviews

                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<GenreDTO>> GetGenresAsync()
        {
            return await _context.Genres
                .Select(g => new GenreDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                    Slug = g.Slug
                })
                .ToListAsync();
        }

        public async Task<List<PlatformDTO>> GetPlatformsAsync()
        {
            return await _context.Platforms
                .Select(p => new PlatformDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    Abbreviation = p.Abbreviation
                })
                .ToListAsync();
        }
    }
}
