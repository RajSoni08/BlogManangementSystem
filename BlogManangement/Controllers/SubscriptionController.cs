using AutoMapper;
using Data;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace BlogManangementAPI.Controllers
{
    [Route("api/SubscriptionController")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository _repository;
        private readonly IBlogRepository _blogRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SubscriptionController(ISubscriptionRepository repository, IBlogRepository blogRepository, ApplicationDbContext context, IMapper mapper)
        {
            _repository = repository;
            _blogRepository = blogRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSubscriptions()
        {
            IEnumerable<Subscription> blog = _repository.GetAll().ToList();
            return Ok(blog);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int? id)
        {
            var blog = _repository.Get(u => u.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }


        [HttpPost]
        public IActionResult CreateSubscription([FromBody] SubsrciptionDTO subscrip)
        {
            if (subscrip == null) {
                return BadRequest();
            }
            var blog = _context.Blogs.FirstOrDefault(u => u.Id == subscrip.BlogId);
            if (blog == null)
                return NotFound();

            if (blog.NoofSubsciption <= 0)
                return BadRequest("No available subscriptions for this blog.");

            var subscription = new Subscription
            {
                BlogId = subscrip.BlogId,
                //Email = subscrip.Email

            };
            
            _repository.Create(subscription);
            blog.NoofSubsciption--;
            _blogRepository.Update(blog);
            Subscription Sub = _mapper.Map<Subscription>(subscrip);
            _repository.SaveAs();
            return Ok(subscription);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSubscription(int subscriptionId)
        {
            var subscription = _repository.Get(u => u.Id == subscriptionId);
            if (subscription == null)
                return NotFound();

            _repository.Remove(subscription);

            var blog = _context.Blogs.FirstOrDefault(u => u.Id == subscription.BlogId);
            if (blog != null)
            {
                blog.NoofSubsciption++;
                _blogRepository.Update(blog);
                _repository.SaveAs();
            }

            return Ok();
        }
    }
}

