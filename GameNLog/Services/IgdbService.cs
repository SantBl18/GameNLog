using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using GameNLog.DTOs;

namespace GameNLog.Services
{
    public class IgdbService
    {
        private const int PageSize = 500;
        private const int RateLimit = 260;
        private const string ImageBaseUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/";

        private readonly HttpClient _http;
        //private readonly Ilogger<IgdbService> _logger

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public IgdbService(HttpClient http)
        {
            _http = http;
        }

        public Task<List<IgdbGenre>> FetchAllGenresAsync(CancellationToken ct = default)
            => FetchAllAsync<IgdbGenre>(
                endpoint: "genres",
                fields: "fields id, name, slug;",
                filter: "where name != null & slug != null;",
                ct: ct
                );
        public Task<List<IgdbPlatform>> FetchAllPlatformsAsync(CancellationToken ct = default)
           => FetchAllAsync<IgdbPlatform>(
               endpoint: "platforms",
               fields: "fields id, name, slug, abbreviation;",
               filter: "where name != null & abbreviation != null;",
               ct: ct
               );
        public Task<List<IgdbCompany>> FetchAllCompaniesAsync(CancellationToken ct = default)
          => FetchAllAsync<IgdbCompany>(
              endpoint: "companies",
              fields: "fields id, name, description, slug;",
              filter: "where name != null & description != null & slug != null;",
              ct: ct
              );
        public Task<List<IgdbGame>> FetchAllGamesAsync(CancellationToken ct = default)
          => FetchAllAsync<IgdbGame>(
              endpoint: "games",
              fields: "fields id, slug, name, cover.id, cover.image_id, summary, genres, platforms, involved_companies.company, parent_game, first_release_date;",
              filter: "where parent_game = null & summary != null & first_release_date != null & cover.image_id != null;",
              ct: ct
              );

        private async Task<List<T>> FetchAllAsync<T>(
            string endpoint,
            string fields,
            string filter,
            CancellationToken ct
            )
        {
            var results = new List<T>();
            int offset = 0;
            while (true)
            {
                var query = BuildQuery(fields, filter, offset);
                var page = await FetchPageAsync<T>(endpoint, query, ct);

                if (page is null || page.Count == 0)
                    break;
                results.AddRange(page);

                if (page.Count < PageSize)
                    break;
                offset += PageSize;
                await Task.Delay(RateLimit, ct);
            }
            return results;
        }

        private async Task<List<T>?> FetchPageAsync<T>(string endpoint, string query, CancellationToken ct)
        {
            var content = new StringContent(query, Encoding.UTF8, "text/plain");
            var response = await _http.PostAsync(endpoint, content, ct);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync(ct);
                response.EnsureSuccessStatusCode();
            }
            var stream = await response.Content.ReadAsStreamAsync(ct);
            return await JsonSerializer.DeserializeAsync<List<T>>(stream, _jsonOptions, ct);
        }

        private static string BuildQuery(string fields, string filter, int offset)
        {
            var sb = new StringBuilder();
            sb.AppendLine(fields);

            if (!string.IsNullOrEmpty(filter))
            {
                sb.AppendLine(filter);
            }
            sb.AppendLine($"limit {PageSize};");
            sb.AppendLine($"offset {offset};");
            return sb.ToString();
        }
        public static string BuildCoverUrl(string imageId)
        => $"{ImageBaseUrl}{imageId}.jpg";

    }
}
