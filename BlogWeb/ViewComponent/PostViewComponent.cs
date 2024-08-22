using BlogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWeb.ViewComponents
{
    public class PostViewComponent : ViewComponent
    {
        private readonly BlogWebContext _context;

        public PostViewComponent(BlogWebContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isFeatured = false, bool isTrending = false)
        {
            IQueryable<Post> query = _context.Posts
                .Include(p => p.User)          // Include User data
                .Include(p => p.Category)      // Include Category data if needed
                .AsQueryable();

            if (isFeatured)
            {
                query = query.Where(p => p.IsFeatured);
            }

            if (isTrending)
            {
                query = query.Where(p => p.IsTrend);
            }

            var posts = await query.ToListAsync();
            return View(posts ?? new List<Post>());
        }
    }
}
