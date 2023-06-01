using Models;

namespace BlogManagementWeb.Service.IService
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> GetUsersAsync();
        Task<Subscription> GetUserByIdAsync(int id);
        Task<Subscription> CreateUserAsync(Subscription user);
     
        Task DeleteUserAsync(int id);
    }
}
