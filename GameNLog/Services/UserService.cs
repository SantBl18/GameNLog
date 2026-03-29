using GameNLog.Data;
using GameNLog.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameNLog.Services
{
    public class UserService
    {
        private readonly GameNLogContext _context;
        const string igdbURL = "https://images.igdb.com/igdb/image/upload/t_cover_big/";

        public UserService(GameNLogContext context)
        {
            _context = context;
        }
        public async Task<UserProfileDTO> GetUserProfile(int id)
        {
            var userInfo = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserInfoDTO
                {
                    Id = id,
                    Username = u.Username,
                    Biography = u.Biography
                })
                .FirstOrDefaultAsync();

            var recentlyReviewed = await _context.GameReviews
                .Where(gr => gr.Id == id)
                .OrderByDescending(r => r.ReviewedAt)
                .Take(5)
                .Select(gr => new ProfileReviewDTO
                {
                    Id = gr.Id,
                    Rating = gr.Score,
                    Description = gr.Description,
                    CreatedAt = gr.ReviewedAt,
                    Game = new ProfileGameSummaryDTO
                    {
                        Id = gr.PlayedGame.GameID,
                        Name = gr.PlayedGame.Game.Name,
                        CoverURL = igdbURL + gr.PlayedGame.Game.Cover.ImageID.ToString() + ".jpg"
                    }
                })
                .ToListAsync();

            var recentlyPlayed = await _context.PlayedGames
                .Where(pg => pg.UserID == id)
                .OrderByDescending(pg => pg.LoggedAt)
                .Take(5)
                .Select(pg => new GameSummaryDTO
                {
                    Id = pg.GameID,
                    Name = pg.Game.Name,
                    CoverURL = igdbURL + pg.Game.Cover.ImageID.ToString() + ".jpg",
                    AverageRating = pg.Reviews
                        .Average(r => r.Score)

                })
                .ToListAsync();

            return new UserProfileDTO
            {
                UserInfo = userInfo,
                RecentlyReviewed = recentlyReviewed,
                RecentlyPlayed = recentlyPlayed
            };
        }
    }
}
