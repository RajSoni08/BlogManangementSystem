using BlogManagementWeb.Service;
using BlogManagementWeb.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BlogManagementWeb.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _SubscriptionService;
        public SubscriptionController(ISubscriptionService SubscriptionService)
        {
            _SubscriptionService = SubscriptionService;
        }
        public async Task<IActionResult> Index()
        {
            var posts = await _SubscriptionService.GetUsersAsync();
            return View(posts);
        }
        public IActionResult Subscribe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                var createdBlog = await _SubscriptionService.CreateUserAsync(subscription);
                return RedirectToAction(nameof(Index), new { id = createdBlog.Id });
            }

            return View(subscription);
        }
        public async Task<IActionResult> UnSubscribe(int id)
        {
            var blog = await _SubscriptionService.GetUserByIdAsync(id);
            return View(blog);
        }

        [HttpPost]
        [ActionName("UnSubscribe")]
        public async Task<IActionResult> UnSubscribeConfirmed(int id)
        {
            await _SubscriptionService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
