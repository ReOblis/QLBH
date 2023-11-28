using OfficeOpenXml.Style;

namespace QLBH.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int NovelID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Novel Novel { get; set; }
    }
}
