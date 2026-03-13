namespace GameNLog.Models
{
    public class GamePlatform
    {
        public int GameID { get; set; }
        public int PlatformID { get; set; }
        public required Game Game { get; set; }
        public required Platform Platform { get; set; }
    }
}
