using GameNLog.Data;
using GameNLog.DTOs;
using GameNLog.Models;
using Microsoft.EntityFrameworkCore;
using PhenX.EntityFrameworkCore.BulkInsert.Extensions;
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
            var companies = igdbCompanies
                .Select(igCompany => new Company
                {
                    CompanyID = igCompany.Id,
                    Name = igCompany.Name,
                    Description = igCompany.Description,
                    Slug = igCompany.Slug
                });
            await _db.ExecuteBulkInsertAsync(companies, cancellationToken: ct);
        }

        private async Task SyncGamesAsync(CancellationToken ct)
        {
            var igdbGames = (await _igdb.FetchAllGamesAsync(ct))
                .DistinctBy(g => g.Id)
                .ToList();

            var validPlatformIds = await _db.Platforms.Select(p => p.PlatformID).ToHashSetAsync();
            var validGenreIds = await _db.Genres.Select(g => g.GenreId).ToHashSetAsync();
            var validCompanyIds = await _db.Companies.Select(p => p.CompanyID).ToHashSetAsync();

            var covers = igdbGames
                .Select(igGame => new Cover
                {
                    CoverID = igGame.Cover.Id,
                    ImageID = igGame.Cover.ImageId!
                })
                .DistinctBy(c => c.CoverID)
                .ToList();
            await _db.ExecuteBulkInsertAsync(covers, cancellationToken: ct);
            var insertedCoverIds = covers.Select(c => c.CoverID).ToHashSet();

            var games = igdbGames
                .Where(g => insertedCoverIds.Contains(g.Cover.Id))
                .Select(igGame => new Game
                {
                    GameID = igGame.Id,
                    Name = igGame.Name,
                    Slug = igGame.Slug,
                    Summary = igGame.Summary,
                    CoverID = igGame.Cover.Id,
                    FirstReleaseDate = DateTimeOffset.FromUnixTimeSeconds(igGame.FirstReleaseDate).UtcDateTime
                })
                .ToList();
            games.Take(5).ToList().ForEach(g => Console.WriteLine($"GameID: {g.GameID}, CoverID: {g.CoverID}"));
            await _db.ExecuteBulkInsertAsync(games, cancellationToken: ct);

            // inserting in joint tables
            var gameGenres = igdbGames.SelectMany(igGame =>
                igGame.Genres
                .Select(genreId => new GameGenre
                {
                    GameID = igGame.Id,
                    GenreID = genreId
                })
            );
            await _db.ExecuteBulkInsertAsync(gameGenres, cancellationToken: ct);

            var gamePlatforms = igdbGames.SelectMany(igGame =>
                igGame.Platforms
                .Where(validPlatformIds.Contains)
                .Select(platformId => new GamePlatform
                {
                    GameID = igGame.Id,
                    PlatformID = platformId
                })
            );
            await _db.ExecuteBulkInsertAsync(gamePlatforms, cancellationToken: ct);

            var involvedCompanies = igdbGames.SelectMany(igGame =>
                igGame.InvolvedCompanies
                .DistinctBy(ic => ic.Company)
                .Select(ic => ic.Company)
                .Where(validCompanyIds.Contains)
                .Select(companyId => new InvolvedCompany
                {
                    GameID = igGame.Id,
                    CompanyID = companyId
                })
            );
            await _db.ExecuteBulkInsertAsync(involvedCompanies, cancellationToken: ct);
        }
    }
}
