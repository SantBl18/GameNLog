namespace GameNLog.Models
{
    public class GamePlatform
    {
        public int GameID { get; set; }
        public int PlatformID { get; set; }
        public Game? Game { get; set; }
        public Platform? Platform { get; set; }
    }
}
