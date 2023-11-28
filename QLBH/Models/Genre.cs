namespace QLBH.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string Name { get; set; }

        // Navigation properties
        public ICollection<NovelGenre> NovelGenres { get; set; }
    }
}
