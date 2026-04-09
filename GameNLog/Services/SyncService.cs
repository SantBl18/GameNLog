using GameNLog.Data;
using GameNLog.DTOs;
using GameNLog.Models;
using Microsoft.EntityFrameworkCore;
using PhenX.EntityFrameworkCore.BulkInsert.Extensions;
using PhenX.EntityFrameworkCore.BulkInsert.Options;

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
            var existingGenreIds = await _db.Genres.Select(g => g.Id).ToHashSetAsync(ct);
            foreach (var igGenre in igdbGenres)
            {
                if (!existingGenreIds.Contains(igGenre.Id))
                {
                    _db.Genres.Add(new Genre { 
                        Id = igGenre.Id, Name = igGenre.Name, Slug = igGenre.Slug 
                    });
                }
            }

            await _db.SaveChangesAsync(ct);
        }

        private async Task SyncPlatformsAsync(CancellationToken ct)
        {
            var igdbPlatforms = await _igdb.FetchAllPlatformsAsync(ct);
            var existingCompanyIds = await _db.Platforms.Select(p => p.Id).ToHashSetAsync(ct);

            foreach (var igPlatform in igdbPlatforms)
            {
                if (!existingCompanyIds.Contains(igPlatform.Id))
                {
                    _db.Platforms.Add(new Platform {
                        Id = igPlatform.Id,
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
            await foreach (var igdbCompanies in _igdb.FetchCompanyPagesAsync(ct))
            {
                var companies = igdbCompanies
                    .Select(igCompany => new Company
                    {
                        Id = igCompany.Id,
                        Name = igCompany.Name,
                        Description = igCompany.Description,
                        Slug = igCompany.Slug
                    });

                await _db.ExecuteBulkInsertAsync(companies, cancellationToken: ct, onConflict:
                    new OnConflictOptions<Company>
                    {
                        Match = c => new
                        {
                            c.Id
                        },
                    });
            }
           
        }

        private async Task SyncGamesAsync(CancellationToken ct)
        {

            await foreach (var igdbGames in _igdb.FetchGamePagesAsync(ct))
            {
                var games = igdbGames
                    .Select(igGame => new Game
                    {
                        Id = igGame.Id,
                        Name = igGame.Name,
                        Slug = igGame.Slug,
                        Summary = igGame.Summary,
                        FirstReleaseDate = igGame.FirstReleaseDate is not null ?
                            DateTimeOffset.FromUnixTimeSeconds(igGame.FirstReleaseDate.Value).UtcDateTime
                            : null
                    });
                await _db.ExecuteBulkInsertAsync(games, cancellationToken: ct, onConflict:
                    new OnConflictOptions<Game>
                    {
                        Match = g => new
                        {
                            g.Id
                        }
                    });

                var covers = igdbGames
                    .Where(igGame => igGame.Cover is not null)
                    .Select(igGame => new GameCover
                    {
                        Id = igGame.Cover!.Id,
                        ImageId = igGame.Cover.ImageId,
                        GameId = igGame.Id
                    });
                await _db.ExecuteBulkInsertAsync(covers, cancellationToken: ct, onConflict:
                    new OnConflictOptions<GameCover>
                    {
                        Match = gc => new
                        {
                            gc.Id
                        }
                    });

                var gameGenres = igdbGames.SelectMany(igGame =>
                    igGame.Genres
                    .Select(genreId => new GameGenre
                    {
                        GameId = igGame.Id,
                        GenreId = genreId
                    }));
                await _db.ExecuteBulkInsertAsync(gameGenres, cancellationToken: ct, onConflict:
                    new OnConflictOptions<GameGenre>
                    {
                        Match = gr => new
                        {
                            gr.GameId,
                            gr.GenreId
                        }
                    });

                var gamePlatforms = igdbGames.SelectMany(igGame =>
                    igGame.Platforms
                    .Select(platformId => new GamePlatform
                    {
                        GameId = igGame.Id,
                        PlatformId = platformId
                    }));
                await _db.ExecuteBulkInsertAsync(gamePlatforms, cancellationToken: ct, onConflict:
                    new OnConflictOptions<GamePlatform>
                    {
                        Match = gr => new
                        {
                            gr.GameId,
                            gr.PlatformId
                        }
                    });

                var involvedCompanies = igdbGames.SelectMany(igGame =>
                    igGame.InvolvedCompanies
                    .Select(ic => ic.Company)
                    .Select(companyId => new InvolvedCompany
                    {
                        GameId = igGame.Id,
                        CompanyId = companyId
                    }));

                await _db.ExecuteBulkInsertAsync(involvedCompanies, cancellationToken: ct, onConflict:
                    new OnConflictOptions<InvolvedCompany>
                    {
                        Match = gr => new
                        {
                            gr.GameId,
                            gr.CompanyId
                        }
                    });
            }

        }
    }
}
