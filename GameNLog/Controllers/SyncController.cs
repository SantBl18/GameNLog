using GameNLog.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameNLog.Controllers
{
    [ApiController]
    [Route("api/sync")]
    public class SyncController : ControllerBase
    {
        private readonly SyncService _sync;

        public SyncController(SyncService sync)
        {
            _sync = sync;
        }

        [HttpPost]
        public async Task<IActionResult> TriggerSync(CancellationToken ct)
        {
            try
            {
                await _sync.SyncAllASync(ct);
                return Ok(new { message = "Sync completed succefully" });
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, new { message = "Sync cancelled." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { message = "Failed to reach IGDB API.", detail = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sync failed.", detail = ex.Message });
            }
        }
    }
}
