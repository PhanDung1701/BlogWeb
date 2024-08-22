using BlogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null)
            {
                return NotFound();
            }
            return View("DetailPost", post);
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
