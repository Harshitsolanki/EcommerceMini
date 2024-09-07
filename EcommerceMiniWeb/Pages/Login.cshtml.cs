using EcommerceMini.Application;
using EcommerceMini.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceMiniWeb.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserAppService _userAppService;
        private readonly ILogger<LoginModel> _logger;
        private readonly IConfiguration _configuration;
        [BindProperty]
        public LoginDto LoginDto { get; set; }
        public LoginModel(ILogger<LoginModel> logger, IConfiguration configuration, IUserAppService userAppService)
        {
            _logger = logger;
            _configuration = configuration;
            _userAppService = userAppService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

           var result= await _userAppService.GetUserByEmailPass(LoginDto.Email,LoginDto.Password);
            if (result != null && result.Success)
            {
                var token = GenerateToken(result.Data.Id, LoginDto.Email);
                Response.Cookies.Append("AuthToken", token); // Store JWT token in a cookie
                return RedirectToPage("/Product/index");
            }
            else
            {
                TempData["Message"] = "Login Failed Invalid credentials!";
                return RedirectToPage("/login");
            }
            
        }
        private string GenerateToken(int userId, string Email)
        {
            string secret = _configuration["Jwt:Key"];
            var key = Encoding.UTF8.GetBytes(secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"], // Add Issuer
                Audience = _configuration["Jwt:Audience"], // Add Audience
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }

}
