using BlogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWeb.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogWebContext _context;

        public PostsController(BlogWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Comments) // Bao gồm các bình luận liên quan đến bài viết
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null)
            {
                return NotFound();
            }
            return View("DetailPost", post);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] Comment model)
        {
            if (string.IsNullOrWhiteSpace(model.Content) || string.IsNullOrWhiteSpace(model.Username))
            {
                return Json(new { success = false, message = "Tên và nội dung bình luận không được bỏ trống." });
            }

            var comment = new Comment
            {
                PostId = model.PostId,
                Username = model.Username,
                Content = model.Content,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                username = comment.Username,
                content = comment.Content,
                createdAt = comment.CreatedAt?.ToString("MMMM dd, yyyy hh:mm tt")
            });
        }

        public IActionResult Category(int id)
        {
            var posts = _context.Posts
                .Where(p => p.CategoryId == id && p.IsActive)
                .Include(p => p.Category)
                .Include(p => p.User)
                .ToList();

            return View(posts);
        }
    }
}
