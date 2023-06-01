using BlogManagementWeb.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Data;

namespace BlogManagementWeb.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }


        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.GetUsersAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            Models.Blog post = await _blogService.GetUserByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Blog blog)
        {
            if (ModelState.IsValid)
            {
                var createdBlog = await _blogService.CreateUserAsync(blog);
                return RedirectToAction(nameof(Index), new { id = createdBlog.Id });
            }

            return View(blog);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _blogService.GetUserByIdAsync(id);
            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Models.Blog blog)
        {
            if (ModelState.IsValid)
            {
                await _blogService.UpdateUserAsync(id, blog);
                return RedirectToAction(nameof(Index), new { id });
            }

            return View(blog);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogService.GetUserByIdAsync(id);
            return View(blog);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _blogService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
        //public IActionResult PendingBlogs()
        //{
        //    IEnumerable<Blog> pendingBlogs = _blogService.GetPendingBlogs();
        //    return View(pendingBlogs);
        //}
    }
}
