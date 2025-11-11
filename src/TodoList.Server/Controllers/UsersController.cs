using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Data.Entities;
using TodoList.Server.Services.UserService;
using TodoList.Shared.User;

namespace TodoList.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserRegisterResponse>> Register(UserRegisterRequest userRegister)
        {
            var user = new User()
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                Name = userRegister.Name
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

        [HttpPost("Refresh")]
        public async Task<ActionResult<UserLoginResponse>> RefreshToken()
        {
            User? user = await _userService.GetUser();

            if (user == null)
            {
                return NotFound();
            }

            var refreshRequest = new UserLoginRequest() { Email = user.Email! };
            var refreshResponse = await BuildToken<UserLoginRequest, UserLoginResponse>(refreshRequest);
            return refreshResponse;
        }


        private async Task<ActionResult<TResponse>> BuildToken<TRequest, TResponse>(TRequest request)
            where TRequest : CredentialsRequest
            where TResponse : AuthenticationResponse, new()
        {
            var claims = new List<Claim>
            {
                new Claim("email", request.Email)
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
