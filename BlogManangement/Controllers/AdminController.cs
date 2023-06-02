using AutoMapper;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BlogManangementAPI.Controllers
{
    [Route("api/AdminController/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        public AdminController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
            
        }
        [HttpGet]
        public IActionResult GetAllNoActionTakenBlogs()
        {
            IEnumerable<Blog> blog = _blogRepository.GetAll(b=>b.IsApproved==false&&b.IsRejected==false);
            if (blog == null) 
            {
                return NotFound();

            }
            else 
            {
                return Ok(blog);
            }
            
        }
        [HttpGet]
        public IActionResult ApprovedBlog(int id)
        {
            if (id == 0)
            {
                return BadRequest();

            }

            Blog blog = _blogRepository.Get(b=>b.Id==id);
            if (blog == null)
            {
                return NotFound();

            }
            blog.IsApproved= true;
            _blogRepository.SaveAs();
            return Ok(blog);
        }
        [HttpGet]
        public IActionResult RejectBlog(int id)
        {
            if (id == 0)
            {
                return BadRequest();

            }

            Blog blog = _blogRepository.Get(b => b.Id == id);
            if (blog == null)
            {
                return NotFound();

            }
            blog.IsRejected = true;
            _blogRepository.SaveAs();
            return Ok(blog);
        }


    }
}
