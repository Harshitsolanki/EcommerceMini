using EcommerceMini.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceMini.Application
{
    public interface IUserAppService
    {
        public Task<ResponseDto<List<UserDto>>> GetUsers();
        public Task<ResponseDto<UserDto>> CreateUser(RegisterDto registerDto);
        public Task<ResponseDto<UserDto>> UpdateUser(RegisterDto registerDto);
        public Task<ResponseDto<UserDto>> DeleteUser(int userId);
        public Task<ResponseDto<UserDto>> GetUserById(int userId);
        public Task<ResponseDto<UserDto>> GetUserByEmailPass(string email, string password);
    }
}
