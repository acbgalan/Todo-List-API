using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TodoList.Data.Entities;

namespace TodoList.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User?> GetUser()
        {
            var emailClaim = _httpContextAccessor.HttpContext!.User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim == null)
            {
                return null;
            }

            return await _userManager.FindByEmailAsync(emailClaim.Value);
        }

        public bool IsAdministrator()
        {
            var administratorClaim = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "Administrator");
            return administratorClaim != null ? true : false;
        }
    }
}
