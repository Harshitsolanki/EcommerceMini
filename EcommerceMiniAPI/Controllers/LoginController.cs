using EcommerceMini.Application;
using EcommerceMini.Application.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace EcommerceMiniAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserAppService _userAppService;
        private readonly ILogger<LoginController> _logger;
        private ResponseDto<LoginResponseDto> responseDto;
        private readonly IConfiguration _configuration;
        public LoginController(IUserAppService userAppService, IConfiguration configuration)
        {
            _userAppService = userAppService;            
            _configuration = configuration;
            responseDto = new ResponseDto<LoginResponseDto>();
        }
        [HttpPost]
        public async Task<ResponseDto<LoginResponseDto>> TokenAuth(LoginDto loginDto)
        {
            try
            {
                var loginResult = await _userAppService.GetUserByEmailPass(loginDto.Email, loginDto.Password);
                if (loginResult.Success)
                {
                    LoginResponseDto loginResponseDto = new LoginResponseDto();
                    loginResponseDto.Email = loginResult.Data.Email;
                    loginResponseDto.UserId = loginResult.Data.Id;
                    var Token = GenerateToken(loginResponseDto.UserId, loginResponseDto.Email);
                    loginResponseDto.AccessToken = Token;
                    responseDto.Success = true;
                    responseDto.Data = loginResponseDto;
                    return responseDto;
                }
                else
                {
                    
                    responseDto.Success = false;
                    responseDto.Message = "Invalid Credentials";
                    return responseDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login Error");
                responseDto.Success = false;
                responseDto.Message = ex.Message;
                return responseDto;
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
