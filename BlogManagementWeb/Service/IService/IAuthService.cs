using Models;

namespace BlogManagementWeb.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO model);
    }
}
