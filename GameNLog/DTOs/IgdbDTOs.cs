namespace GameNLog.DTOs
{
    public class IgdbGame
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Summary { get; set; }
        public double? Rating { get; set; }
        public long? FirstReleaseDate { get; set; }
        public int? Cover { get; set; }            
        public List<int>? Genres { get; set; }
        public List<int>? Platforms { get; set; }
        public List<IgdbInvolvedCompany>? InvolvedCompanies { get; set; }
    }

    public class IgdbInvolvedCompany
    {
        public int Id { get; set; }
        public int? Company { get; set; }   
        public bool Developer { get; set; }
        public bool Publisher { get; set; }
    }

    public class IgdbGenre
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class IgdbPlatform
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Abbreviation { get; set; }
    }

    public class IgdbCompany
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
    }

    public class IgdbCover
    {
        public int Id { get; set; }
        public int? Game { get; set; }         
        public string? ImageId { get; set; } 
    }
}
