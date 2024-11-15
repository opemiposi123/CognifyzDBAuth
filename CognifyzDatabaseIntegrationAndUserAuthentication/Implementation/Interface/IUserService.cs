using CognifyzDatabaseIntegrationAndUserAuthentication.Dto;
using CognifyzDatabaseIntegrationAndUserAuthentication.Models;

namespace CognifyzDatabaseIntegrationAndUserAuthentication.Implementation.Interface
{
    public interface IUserService
    {
        Task<Status> Login(LoginModel login);
        Task LogoutAsync();
        Task<List<UserDto>> GetAllUser();
        Task<UserDto> GetUserDetail(string id);
    }
}
