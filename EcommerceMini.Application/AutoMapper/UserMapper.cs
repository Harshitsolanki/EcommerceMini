using AutoMapper;
using EcommerceMini.Application.Dto;
using EcommerceMini.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EcommerceMini.Application.AutoMapper
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true)); // Default IsActive to true when mapping

            CreateMap<User, RegisterDto>(); // For reverse mapping, if needed
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true)); // Default IsActive to true when mapping

            CreateMap<User, UserDto>(); // For reverse mapping, if needed

        }
    }
}
