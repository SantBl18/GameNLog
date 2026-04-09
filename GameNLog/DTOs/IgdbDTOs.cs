using System.Text.Json.Serialization;

namespace GameNLog.DTOs
{
    public class IgdbGame
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Summary { get; set; }
        [JsonPropertyName("first_release_date")]
        public long? FirstReleaseDate { get; set; }
        public IgdbCover? Cover { get; set; }
        public List<int> Genres { get; set; } = [];
        public List<int> Platforms { get; set; } = [];
        [JsonPropertyName("involved_companies")]
        public List<IgdbInvolvedCompany> InvolvedCompanies { get; set; } = [];
    }

    public class IgdbInvolvedCompany
    {
        public int Id { get; set; }
        public int Company { get; set; }   
    }

    public class IgdbGenre
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
    }

    public class IgdbPlatform
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Abbreviation { get; set; }

    }

    public class IgdbCompany
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Description { get; set; }
    }

    public class IgdbCover
    {
        public int Id { get; set; }
        public int Game { get; set; }
        [JsonPropertyName("image_id")]
        public required string ImageId { get; set; } 
    }
}
