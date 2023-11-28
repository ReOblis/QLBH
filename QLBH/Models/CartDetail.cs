namespace QLBH.Models
{
    public class CartDetail
    {
        public int CartDetailID { get; set; }
        public int CartID { get; set; }
        public int NovelID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation properties
        public Cart Cart { get; set; }
        public Novel Novel { get; set; }
    }
}
