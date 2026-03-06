using GameNLog.DTOs;

namespace GameNLog.Services
{
    public interface IGameService
    {
        Task<List<GameSummaryDTO>> GetGamesAsync();
        Task<GameDetailDTO?> GetGameByIdAsync(int id);
    }
}
