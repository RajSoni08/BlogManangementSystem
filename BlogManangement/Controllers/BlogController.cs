using AutoMapper;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Data;

namespace BlogManangementAPI.Controllers
{
    [Route("api/BlogController")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogController(IBlogRepository blogRepository,IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Blog> blog = _blogRepository.GetAll().ToList();
            return Ok(blog);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int? id)
        {
            var blog = _blogRepository.Get(u => u.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Blog blog)
        {
            _blogRepository.Create(blog);
            _blogRepository.SaveAs();
            return CreatedAtAction(nameof(GetById), new { id = blog.Id }, blog);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Blog blog)
        {
           // Blog blog = _blogRepository.Get(u => u.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            // Perform update logic, e.g., update existingUser properties based on updatedUser
           // Blog model = _mapper.Map<Blog>(blogDTO);
            _blogRepository.Update(blog);
            _blogRepository.SaveAs();
            return Ok(blog);
        }

        [HttpDelete("{id}")]
       // [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            Blog blog = _blogRepository.Get(u => u.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            _blogRepository.Remove(blog);
            _blogRepository.SaveAs();

            return NoContent();
        }

    }
}
