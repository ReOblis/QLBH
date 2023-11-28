namespace QLBH.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
