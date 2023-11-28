
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBH.Data;
namespace QLBH.Controllers
{
    public class NovelController : Controller
    {
      private readonly AppDbContext _context;

        public NovelController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ChucVus
        public async Task<IActionResult> Index()
        {
              return _context.Novels != null ? 
                          View(await _context.Novels.ToListAsync()) :
                          Problem("Entity set 'QLNSContext.ChucVu'  is null.");
        }
        public IActionResult Details(int id)
        {
            // Lấy chi tiết tiểu thuyết từ bảng Novels kèm theo thông tin thể loại và đánh giá
            var novelDetails = _context.Novels
                .Where(n => n.NovelID == id)
                .Include(n => n.NovelGenres)
                .ThenInclude(ng => ng.Genre)
                .Include(n => n.Ratings)  // Include thông tin đánh giá
                .FirstOrDefault();

            if (novelDetails == null)
            {
                return NotFound();
            }

            return View(novelDetails);
        }
    }
}
