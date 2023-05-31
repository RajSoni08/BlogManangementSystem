using Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Blog entity)
        {

            _db.Blogs.Update(entity);


        }
        

        //IEnumerable<Blog> IBlogRepository.GetPendingBlogs()
        //{
        //    return _db.Blogs.Where(blog => !blog.IsApproved && !blog.IsRejected);
        //}
    }
}
