using Models;
using Models.DTO;

namespace BlogManagementWeb.Service.IService
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> GetUsersAsync();
        Task<Subscription> GetUserByIdAsync(int id);
        Task<Subscription> CreateUserAsync(SubsrciptionDTO user);
     
        Task DeleteUserAsync(int id);
    }
}
