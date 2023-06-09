﻿using BlogManagementWeb.Service;
using BlogManagementWeb.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace BlogManagementWeb.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _SubscriptionService;
        private readonly IBlogService _BlogService;
        public SubscriptionController(ISubscriptionService SubscriptionService, IBlogService blogService)
        {

            _SubscriptionService = SubscriptionService;
            _BlogService = blogService;
        }
        public async Task<IActionResult> Index()
        {

            var posts = await _SubscriptionService.GetUsersAsync();
            var blogs = _BlogService.GetUsersAsync();
            ViewBag.Blogs = blogs;
            return View(posts);
        }
        public IActionResult Subscribe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(int Id)
        {
            var blog = await _BlogService.GetUserByIdAsync(Id);
            SubsrciptionDTO subscription = new SubsrciptionDTO
            {
                BlogId = Id,
                // Populate other subscription properties as needed
            };

            //if (ModelState.IsValid)
            
                var createdBlog = await _SubscriptionService.CreateUserAsync(subscription);
            TempData["success"] = "Successfully Subscribed";
            return RedirectToAction(nameof(Index), new { id = createdBlog.Id });
            
            
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
            TempData["success"] = "Unsubscribed Successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
