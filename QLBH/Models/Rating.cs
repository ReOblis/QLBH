namespace QLBH.Models
{
    public class Rating
    {
        public int RatingID { get; set; }
        public int UserID { get; set; }
        public int NovelID { get; set; }
        public int Score { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Novel Novel { get; set; }
    }
}
