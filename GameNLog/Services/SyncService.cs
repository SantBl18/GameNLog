using GameNLog.Data;
using GameNLog.DTOs;
using GameNLog.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

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
            var existingGenreIds = await _db.Genres.Select(g => g.GenreId).ToHashSetAsync(ct);
            foreach (var igGenre in igdbGenres)
            {
                if (!existingGenreIds.Contains(igGenre.Id))
                {
                    _db.Genres.Add(new Genre { 
                        GenreId = igGenre.Id, Name = igGenre.Name, Slug = igGenre.Slug 
                    });
                }
            }

            await _db.SaveChangesAsync(ct);
        }

        private async Task SyncPlatformsAsync(CancellationToken ct)
        {
            var igdbPlatforms = await _igdb.FetchAllPlatformsAsync(ct);
            var existingCompanyIds = await _db.Platforms.Select(p => p.PlatformID).ToHashSetAsync(ct);

            foreach (var igPlatform in igdbPlatforms)
            {
                if (!existingCompanyIds.Contains(igPlatform.Id))
                {
                    _db.Platforms.Add(new Platform {
                        PlatformID = igPlatform.Id,
                        Name = igPlatform.Name,
                        Slug = igPlatform.Slug,
                        Abbreviation = igPlatform.Abbreviation
                    });
                }
            }

            await _db.SaveChangesAsync(ct);
        }

        private async Task SyncCompaniesAsync(CancellationToken ct)
        {
            var igdbCompanies = await _igdb.FetchAllCompaniesAsync(ct);
            var existingCompanyIds = await _db.Companies.Select(c => c.CompanyID).ToHashSetAsync(ct);

            foreach (var igCompany in igdbCompanies)
            {

                if (!existingCompanyIds.Contains(igCompany.Id))
                {
                    _db.Companies.Add(new Company
                    {
                        CompanyID = igCompany.Id,
                        Name = igCompany.Name,
                        Description = igCompany.Description,
                        Slug = igCompany.Slug
                    });
                }
            }
            await _db.SaveChangesAsync(ct);
        }

        private async Task SyncGamesAsync(CancellationToken ct)
        {
            var igdbGames = (await _igdb.FetchAllGamesAsync(ct))
                .DistinctBy(g => g.Id)
                .ToList();

            var existingCoverIds = await _db.Covers.Select(c => c.CoverID).ToHashSetAsync(ct);
            var existingGameIds = await _db.Games.Select(g => g.GameID).ToHashSetAsync(ct);

            var existingGameGenres = await _db.GameGenres.ToListAsync(ct);
            var existingGamePlatforms = await _db.GamePlatforms.ToListAsync(ct);
            var existingInvolvedCompanies = await _db.InvolvedCompanies.ToListAsync(ct);

            foreach (var igGame in igdbGames)
            {

                IgdbCover igdbCover = igGame.Cover;

                Cover? existingCover = null;
                if (!existingCoverIds.Contains(igdbCover.Id))
                {
                    existingCover = new Cover
                    {
                        CoverID = igdbCover.Id,
                        ImageID = igdbCover.ImageId!
                    };
                    _db.Covers.Add(existingCover);
                    existingCoverIds.Add(igdbCover.Id);
                }
                
                if (!existingGameIds.Contains(igGame.Id))
                {
                    _db.Games.Add(new Game
                    {
                        GameID = igGame.Id,
                        Name = igGame.Name,
                        Slug = igGame.Slug,
                        Summary = igGame.Summary,
                        CoverID = igdbCover.Id,
                        FirstReleaseDate = DateTimeOffset.FromUnixTimeSeconds(igGame.FirstReleaseDate).UtcDateTime
                    });
                }
     
            }
            Console.WriteLine($"About to save {_db.ChangeTracker.Entries().Count()} entries");
            await _db.SaveChangesAsync(ct);
            Console.WriteLine("Save completed");

            // inserting in joint tables
            foreach (var igGame in igdbGames)
            {
                var currentGenreIds = existingGameGenres
                    .Where(gg => gg.GameID == igGame.Id)
                    .Select(gg => gg.GenreID);

                var genreIds = igGame.Genres;

                foreach (var genreId in genreIds.Except(currentGenreIds))
                {
                    _db.GameGenres.Add(new GameGenre
                    { 
                        GameID = igGame.Id,
                        GenreID = genreId
                    });
                }

                var currentPlatformIds = existingGamePlatforms
                    .Where(gp => gp.GameID == igGame.Id)
                    .Select(gg => gg.PlatformID);

                var platformIds = igGame.Platforms;
                
                foreach(var platformId in platformIds.Except(currentPlatformIds))
                {
                    _db.GamePlatforms.Add(new GamePlatform
                    {
                        GameID = igGame.Id,
                        PlatformID = platformId
                    });
                }

                var currentInvolvedCompanyIds = existingInvolvedCompanies
                    .Where(ic => ic.GameID == igGame.Id)
                    .Select(ic => ic.CompanyID);

                var involvedCompanies = igGame.InvolvedCompanies
                    .Select(ic => ic.Company);
                foreach(var companyId in involvedCompanies.Except(currentInvolvedCompanyIds))
                {
                    _db.InvolvedCompanies.Add(new InvolvedCompany
                    {
                        GameID = igGame.Id,
                        CompanyID = companyId
                    });
                }
            }
            await _db.SaveChangesAsync(ct);

        }
    }
}
