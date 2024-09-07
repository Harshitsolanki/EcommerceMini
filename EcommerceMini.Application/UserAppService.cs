using AutoMapper;
using EcommerceMini.Application.Dto;
using EcommerceMini.Entities;
using EcommerceMini.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMini.Application
{
    public class UserAppService: IUserAppService
    {
        private readonly EMDbContext _eMDbContext;
        private readonly IMapper _mapper;
        private ResponseDto<UserDto> responseDto;
        private ResponseDto<List<UserDto>> responseDtoList;


        public UserAppService(IMapper mapper,EMDbContext eMDbContext)
        { 
            _mapper = mapper;
            _eMDbContext= eMDbContext;
            responseDto = new ResponseDto<UserDto>();
            responseDtoList = new ResponseDto<List<UserDto>>();
        }
        public async Task<ResponseDto<List<UserDto>>> GetUsers()
        {
            try
            {
                var userList = _eMDbContext.Users.Where(x => x.IsDeleted == false).ToList();
                if (userList != null && userList.Count > 0)
                {
                    var userDtoList = _mapper.Map<List<UserDto>>(userList);
                    responseDtoList.Success = true;                   
                    responseDtoList.Data = userDtoList;
                    return responseDtoList;
                }
                else
                {
                    responseDtoList.Success = true;
                    responseDtoList.Message = "User not found";                    
                    return responseDtoList;

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<ResponseDto<UserDto>> CreateUser(RegisterDto registerDto)
        {
            try
            {
                var AddUser = _mapper.Map<User>(registerDto);
                AddUser.CreatedDate = System.DateTime.Now;
                _eMDbContext.Users.Add(AddUser);
                await _eMDbContext.SaveChangesAsync();
                responseDto.Success = true;
                responseDto.Message = "User Added Successfully";

                return responseDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<ResponseDto<UserDto>> UpdateUser(RegisterDto registerDto)
        {
            try
            {
                var AddUser = _mapper.Map<User>(registerDto);

                AddUser.LastUpdatedDate = System.DateTime.Now;

                User UpdateUser =await _eMDbContext.Users.FirstOrDefaultAsync(u => u.Id == registerDto.Id);
                if (UpdateUser != null)
                {
                    UpdateUser.Name = registerDto.Name;
                    UpdateUser.Email = registerDto.Email;
                    UpdateUser.Password = registerDto.Password;
                    UpdateUser.LastUpdatedDate = System.DateTime.Now;

                    _eMDbContext.Users.Update(UpdateUser);
                    await _eMDbContext.SaveChangesAsync();
                    responseDto.Success = true;
                    responseDto.Message = "User updated Successfully";
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Message = "User not Found";
                }
                return responseDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<ResponseDto<UserDto>> DeleteUser(int userId)
        {
            try
            {   
                User UpdateUser = await _eMDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (UpdateUser != null)
                {
                    UpdateUser.DeletedDate = System.DateTime.Now;
                    UpdateUser.IsDeleted = true;                    
                    _eMDbContext.Users.Update(UpdateUser);
                    await _eMDbContext.SaveChangesAsync();
                    responseDto.Success = true;
                    responseDto.Message = "User Deleted Successfully";
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Message = "User not Found";
                }
                return responseDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<ResponseDto<UserDto>> GetUserById(int userId)
        {
            try
            {
                User _User = await _eMDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (_User != null)
                {  
                    responseDto.Success = true;                    
                    responseDto.Data = _mapper.Map<UserDto>(_User);
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Message = "User not Found";
                }
                return responseDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<ResponseDto<UserDto>> GetUserByEmailPass(string email,string password)
        {
            try
            {
                User _User = await _eMDbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password==password && u.IsDeleted == false && u.IsActive == true);
                if (_User != null)
                {
                    responseDto.Success = true;
                    responseDto.Data = _mapper.Map<UserDto>(_User);
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Message = "User not Found";
                }
                return responseDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
