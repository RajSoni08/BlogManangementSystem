using Models;

namespace BlogManagementWeb.Service.IService
{
    public interface IAdminService
    {
        Task<IEnumerable<Models.Blog>> GetUsersAsync();
        Task<bool> ApprovedBlog(int id);
        Task<bool> RejectBlog(int id);


    }
}
