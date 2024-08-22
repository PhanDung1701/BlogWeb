using BlogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogWeb.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly BlogWebContext _context;

        public CategoryViewComponent(BlogWebContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories ?? new List<Category>());
        }
    }
}
