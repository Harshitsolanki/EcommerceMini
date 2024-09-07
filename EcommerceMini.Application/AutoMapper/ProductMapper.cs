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
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductDto, Product>()
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true)); // Default IsActive to true when mapping

            CreateMap<Product, ProductDto>(); // For reverse mapping, if needed
        }
    }
}
