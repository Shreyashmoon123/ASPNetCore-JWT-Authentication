using AuthAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AuthAPI.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string? _jwtkey;
        private readonly string? _jwtIssuer;
        private readonly string? _jwtAudience;
        private readonly int _jwtExpiry;

        public UserAuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtkey = configuration["Jwt:Key"];
            _jwtIssuer = configuration["Jwt:Issuer"];
            _jwtAudience = configuration["Jwt:Audience"];
            _jwtExpiry = int.Parse(configuration["Jwt:ExpiryMinutes"]);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (registerModel == null
                || string.IsNullOrEmpty(registerModel.Name)
                || string.IsNullOrEmpty(registerModel.Email)
                || string.IsNullOrEmpty(registerModel.Password))
            {
                return BadRequest("Invalid Registration Details");
            }

            var existingUser = await _userManager.FindByEmailAsync(registerModel.Email);

            if (existingUser != null)
            {
                return Conflict("Email Already Exist");
            }

            var user = new ApplicationUser
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
                Name = registerModel.Name
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User Created Successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid Username or Password"
                });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                loginModel.Password,
                false);

            if (!result.Succeeded)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid Username or Password"
                });
            }

            var token = GenerateJWTToken(user);

            return Ok(new
            {
                success = true,
                token
            });
        }

        private string GenerateJWTToken(ApplicationUser user)
        {
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Name", user.Name),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtkey!));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: Claims,
                expires: DateTime.Now.AddMinutes(_jwtExpiry),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}