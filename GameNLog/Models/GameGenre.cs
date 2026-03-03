namespace GameNLog.Models
{
    public class GameGenre
    {
        // gameid and genreid can't be set as composite keys here,
        // must be done in the database context file
        public int GameID { get; set; }
        public int GenreID { get; set; }

    }
}
