using BlogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWeb.ViewComponents
{
    public class LatestPostsViewComponent : ViewComponent
    {
        private readonly BlogWebContext _context;

        public LatestPostsViewComponent(BlogWebContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var latestPosts = await _context.Posts
                .Include(p => p.Category)   // Include Category data
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .ToListAsync();

            return View(latestPosts);
        }
    }
}
