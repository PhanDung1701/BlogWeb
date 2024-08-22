using BlogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWeb.ViewComponents
{
    public class HotPostViewComponent : ViewComponent
    {
        private readonly BlogWebContext _context;

        public HotPostViewComponent(BlogWebContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var hotPosts = await _context.Posts
                .Include(p => p.User)          // Include User data if needed
                .Include(p => p.Category)      // Include Category data if needed
                .Where(p => p.IsFeatured)
                .ToListAsync();

            return View(hotPosts);
        }
    }
}
