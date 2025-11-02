using Microsoft.AspNetCore.Identity;

namespace TodoList.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityUser?> GetUser()
        {
            var emailClaim = _httpContextAccessor.HttpContext!.User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim == null)
            {
                return null;
            }

            return await _userManager.FindByEmailAsync(emailClaim.Value);
        }
    }
}
