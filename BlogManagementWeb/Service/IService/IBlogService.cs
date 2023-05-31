using Models;

namespace BlogManagementWeb.Service.IService
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetUsersAsync();
        Task<Blog> GetUserByIdAsync(int id);
        Task<Blog> CreateUserAsync(Blog user);
        Task<Blog> UpdateUserAsync(int id, Blog updatedUser);
        Task DeleteUserAsync(int id);
        //IEnumerable<Blog> GetPendingBlogs();
    }
}
