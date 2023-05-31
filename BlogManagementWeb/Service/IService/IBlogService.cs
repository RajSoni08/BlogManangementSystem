using Models;
namespace BlogManagementWeb.Service.IService
{
    public interface IBlogService
    {
        Task<IEnumerable<Models.Blog>> GetUsersAsync();
        Task<Models.Blog> GetUserByIdAsync(int id);
        Task<Models.Blog> CreateUserAsync(Models.Blog user);
        Task<Models.Blog> UpdateUserAsync(int id, Models.Blog updatedUser);
        Task DeleteUserAsync(int id);
        //IEnumerable<Blog> GetPendingBlogs();
    }
}
