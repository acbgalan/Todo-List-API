using Microsoft.AspNetCore.Identity;

namespace TodoList.Server.Services.UserService
{
    public interface IUserService
    {
        Task<IdentityUser?> GetUser();
    }
}
