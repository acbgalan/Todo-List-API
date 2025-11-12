using Microsoft.AspNetCore.Identity;
using TodoList.Data.Entities;

namespace TodoList.Server.Services.UserService
{
    public interface IUserService
    {
        Task<User?> GetUser();
        bool IsAdministrator();
    }
}
