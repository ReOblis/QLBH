namespace QLBH.Models
{
    public class Novel
    {
        public int NovelID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverImageURL { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        // Navigation properties
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
        public ICollection<NovelGenre> NovelGenres { get; set; }
    }
}

