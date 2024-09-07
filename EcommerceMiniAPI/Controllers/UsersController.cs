using EcommerceMini.Application;
using EcommerceMini.Application.Dto;
using EcommerceMini.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceMiniAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserAppService _userAppService;
        private readonly ILogger<UsersController> _logger;
        private ResponseDto<UserDto> responseDto;
        private ResponseDto<List<UserDto>> responseDtoList;
        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
            responseDto = new ResponseDto<UserDto>();
            responseDtoList = new ResponseDto<List<UserDto>>();
        }

        [HttpGet]
        public async Task<ResponseDto<List<UserDto>>> GetAll() 
        {
            try
            {
                return await _userAppService.GetUsers();                
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Error Get user");
                responseDtoList.Success = false;
                responseDtoList.Message = ex.Message;
                return responseDtoList;
            }
        }

        [HttpGet("{userId}")]
        public async Task<ResponseDto<UserDto>> GetById(int userId)
        {
            try
            {
                return await _userAppService.GetUserById(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error GetById user");
                responseDto.Success = false;
                responseDto.Message = ex.Message;
                return responseDto;
            }
        }
        [HttpPost]
        public async Task<ResponseDto<UserDto>> Register(RegisterDto registerDto) {
            try
            {
                return await _userAppService.CreateUser(registerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error GetById user");
                responseDto.Success = false;
                responseDto.Message = ex.Message;
                return responseDto;
            }
        }

        [HttpPut("{userId}")]
        public async Task<ResponseDto<UserDto>> Update(int userId,RegisterDto registerDto) {
            try
            {
                if (userId != null && userId != 0)
                {
                    return await _userAppService.UpdateUser(registerDto);
                }
                else
                {   
                    responseDto.Success = false;
                    responseDto.Message = "UserId not passed";
                    return responseDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user");
                responseDto.Success = false;
                responseDto.Message = ex.Message;
                return responseDto;
            }
        }


        [HttpDelete("{userId}")]
        public async Task<ResponseDto<UserDto>> Delete(int userId)
        {
            try
            {
                return await _userAppService.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error delete user");
                responseDto.Success = false;
                responseDto.Message = ex.Message;
                return responseDto;
            }
        }



    }
}
