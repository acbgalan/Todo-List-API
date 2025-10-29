using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Shared.User;

namespace TodoList.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserRegisterResponse>> Register(UserRegisterRequest userRegister)
        {
            var user = new IdentityUser()
            {
                UserName = userRegister.Email,
                Email = userRegister.Email
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password!);

            if (result.Succeeded)
            {
                return await BuildToken<UserRegisterRequest, UserRegisterResponse>(userRegister);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return ValidationProblem();
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest userLogin)
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Incorrect login");
                return ValidationProblem();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user!, userLogin.Password!, false);

            if (result.Succeeded)
            {
                return await BuildToken<UserLoginRequest, UserLoginResponse>(userLogin);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Incorrect login");
                return ValidationProblem();
            }
        }

        private async Task<ActionResult<TResponse>> BuildToken<TRequest, TResponse>(TRequest request)
            where TRequest : CredentialsRequest
            where TResponse : AuthenticationResponse, new()
        {
            var claims = new List<Claim>
            {
                new Claim("email", request.Email),
                new Claim("test", "test value claim")
            };

            var user = await _userManager.FindByEmailAsync(request.Email);
            var claimsDB = await _userManager.GetClaimsAsync(user!);
            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt_key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMonths(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            var response = new TResponse
            {
                Token = token,
                Expiration = expiration
            };

            return Ok(response);
        }


    }
}
