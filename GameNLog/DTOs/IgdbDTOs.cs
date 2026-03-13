namespace GameNLog.DTOs
{
    public class IgdbGame
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Summary { get; set; }
        public required string Slug { get; set; }
        public long FirstReleaseDate { get; set; }
        public int Cover { get; set; }
        public List<int> Genres { get; } = [];
        public List<int> Platforms { get; } = [];
        public List<IgdbInvolvedCompany> InvolvedCompanies { get; } = [];
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
        public required string Abbreviation { get; set; }
        public required string Slug { get; set; }

    }

    public class IgdbCompany
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Slug { get; set; }
    }

    public class IgdbCover
    {
        public int Id { get; set; }
        public int Game { get; set; }         
        public required string ImageId { get; set; } 
    }
}
