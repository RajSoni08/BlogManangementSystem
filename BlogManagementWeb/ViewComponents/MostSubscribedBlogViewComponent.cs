using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementWeb.ViewComponents
{
    public class MostSubscribedBlogViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context; // Replace with your database context

        public MostSubscribedBlogViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Retrieve the most subscribed blog from the database
            var mostSubscribedBlog = await _context.Blogs
                .OrderBy(blog => blog.NoofSubsciption)
                .FirstOrDefaultAsync();

            // Pass the blog title to the view
            return View("Default",mostSubscribedBlog.BlogTitle);
        }

    }
}
