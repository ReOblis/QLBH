namespace QLBH.Models
{
    public class NovelGenre
    {
        public int NovelID { get; set; }
        public int GenreID { get; set; }

        // Navigation properties
        public Novel Novel { get; set; }
        public Genre Genre { get; set; }
    }
}
