using GameNLog.Data;
using GameNLog.Models;
using Microsoft.EntityFrameworkCore;

namespace GameNLog.Services
{
    public class SyncService
    {
        private readonly IgdbService _igdb;
        private readonly GameNLogContext _db;
        
        public SyncService(IgdbService igdb, GameNLogContext db)
        {
            _db = db;
            _igdb = igdb;
        }

        public async Task SyncAllASync(CancellationToken ct = default)
        {
            // all of these methods are for upserting
            await SyncGenresAsync(ct);
            await SyncPlatformsAsync(ct);
            await SyncCompaniesAsync(ct);
            await SyncGamesAsync(ct);
        }

        private async Task SyncGenresAsync(CancellationToken ct)
        {
            var igdbGenres = await _igdb.FetchAllGenresAsync(ct);
            foreach (var igGenre in igdbGenres)
            {
                var existingGenre = await _db.Genres.FindAsync([igGenre.Id], ct);
                if (existingGenre is null)
                {
                    _db.Genres.Add(new Genre 
                    { GenreId = igGenre.Id, Name = igGenre.Name, Slug = igGenre.Slug });
                }
                else if (existingGenre.Name != igGenre.Name)
                {
                    existingGenre.Name = igGenre.Name;
                }
            }

            await _db.SaveChangesAsync(ct);
        }

        private async Task SyncPlatformsAsync(CancellationToken ct)
        {
            var igdbPlatforms = await _igdb.FetchAllPlatformsAsync(ct);

            foreach (var igPlatform in igdbPlatforms)
            {
                var existingPlatform = await _db.Platforms.FindAsync([igPlatform.Id], ct);
                if (existingPlatform is null)
                {
                    _db.Platforms.Add(new Platform {
                        PlatformID = igPlatform.Id,
                        Name = igPlatform.Name,
                        Slug = igPlatform.Slug,
                        Abbreviation = igPlatform.Abbreviation});
                }
                else if (existingPlatform.Name != igPlatform.Name)
                {
                    existingPlatform.Name = igPlatform.Name;
                }
            }

            await _db.SaveChangesAsync(ct);
        }

        // ── Companies ─────────────────────────────────────────────────────────

        private async Task SyncCompaniesAsync(CancellationToken ct)
        {
            var igdbCompanies = await _igdb.FetchAllCompaniesAsync(ct);

            foreach (var igCompany in igdbCompanies)
            {

                var existingCompany = await _db.Companies.FindAsync([igCompany.Id], ct);
                if (existingCompany is null)
                {
                    _db.Companies.Add(new Company {
                        CompanyID = igCompany.Id,
                        Name = igCompany.Name,
                        Description = igCompany.Description,
                        Slug = igCompany.Slug 
                    });
                }
                else if (existingCompany.Name != igCompany.Name)
                {
                    existingCompany.Name = igCompany.Name;
                }
            }

            await _db.SaveChangesAsync(ct);
        }

        private async Task SyncGamesAsync(CancellationToken ct)
        {
            var igdbGames = await _igdb.FetchAllGamesAsync(ct);
            var igdbCovers = await _igdb.FetchAllCoversAsync(ct);

            var existingGameGenres = await _db.GameGenres.ToListAsync(ct);
            var existingGamePlatforms = await _db.GamePlatforms.ToListAsync(ct);
            var existingInvolvedCompanies = await _db.InvolvedCompanies.ToListAsync(ct);

            var igdbCoverByGameId = igdbCovers
                .ToDictionary(c => c.Game);

            foreach (var igGame in igdbGames)
            {
                // first upsert covers
                if (!igdbCoverByGameId.TryGetValue(igGame.Id, out var igdbCover))
                {
                    continue;
                }

                var existingCover = await _db.Covers.FindAsync([igdbCover.Id], ct);
                if (existingCover is null)
                {
                    existingCover = new Cover
                    {
                        CoverID = igdbCover.Id,
                        ImageID = igdbCover.ImageId
                    };
                    _db.Covers.Add(existingCover);

                }
                else if (existingCover.ImageID != igdbCover.ImageId)
                {
                    existingCover.ImageID = igdbCover.ImageId;
                }

                await _db.SaveChangesAsync(ct);
             

                
                // now upsert games
                var existingGame = await _db.Games.FindAsync([igGame.Id], ct);
                if (existingGame is null)
                {
                    _db.Games.Add(new Game
                    {
                        GameID = igGame.Id,
                        Name = igGame.Name,
                        Slug = igGame.Slug,
                        Summary = igGame.Summary,
                        Cover = existingCover
                    });
                }
                else 
                {
                    existingGame.Name = igGame.Name;
                    existingGame.Slug = igGame.Slug;
                    existingGame.Summary = igGame.Summary;
                    existingGame.Cover = existingCover;
                }


            }
        }
    }


}
