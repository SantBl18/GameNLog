using GameNLog.DTOs;

namespace GameNLog.Services
{
    public interface IGameService
    {
        Task<PaginatedResultDTO<GameSummaryDTO>> GetGamesAsync(GameFilterDTO filter);
        Task<GameDetailDTO?> GetGameByIdAsync(int id);
        Task<List<GenreDTO>> GetGenresAsync();
        Task<List<PlatformDTO>> GetPlatformsAsync();
    }
}
