using OfficeOpenXml.Style;

namespace QLBH.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; }

        // Navigation properties
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
