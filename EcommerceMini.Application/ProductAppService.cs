using AutoMapper;
using EcommerceMini.Application.Dto;
using EcommerceMini.Entities;
using EcommerceMini.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceMini.Application
{
    public class ProductAppService : IProductAppService
    {
        private readonly EMDbContext _eMDbContext;
        private readonly IMapper _mapper;
        private ResponseDto<ProductDto> responseDto;
        private ResponseDto<List<ProductDto>> responseDtoList;


        public ProductAppService(IMapper mapper, EMDbContext eMDbContext)
        {
            _mapper = mapper;
            _eMDbContext = eMDbContext;
            responseDto = new ResponseDto<ProductDto>();
            responseDtoList = new ResponseDto<List<ProductDto>>();
        }

        public async Task<ResponseDto<List<ProductDto>>> GetProducts()
        {
            try
            {
                var productsList = _eMDbContext.Products.Where(x=> x.IsDeleted==false).ToList();
                if (productsList != null && productsList.Count > 0)
                {
                    var productDtoList = _mapper.Map<List<ProductDto>>(productsList);
                    responseDtoList.Success = true;
                    responseDtoList.Data = productDtoList;
                    return responseDtoList;
                }
                else
                {
                    responseDtoList.Success = false;
                    responseDtoList.Message = "Product not found";
                    return responseDtoList;

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<ResponseDto<ProductDto>> Create(ProductDto productDto)
        {
            try
            {
                var addProduct = _mapper.Map<Product>(productDto);
                addProduct.CreatedDate = System.DateTime.Now;
                _eMDbContext.Products.Add(addProduct);
                await _eMDbContext.SaveChangesAsync();
                responseDto.Success = true;
                responseDto.Message = "Product Added Successfully";

                return responseDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<ResponseDto<ProductDto>> Update(ProductDto productDto)
        {
            try
            {
                Product updateProduct = await _eMDbContext.Products.FirstOrDefaultAsync(u => u.Id == productDto.Id);
                if (updateProduct != null)
                {
                    updateProduct.Name = productDto.Name;
                    updateProduct.Sku = productDto.Sku;
                    updateProduct.Description = productDto.Description;
                    updateProduct.Price = productDto.Price;
                    updateProduct.IsActive = productDto.IsActive;
                    updateProduct.LastUpdatedDate = System.DateTime.Now;

                    _eMDbContext.Products.Update(updateProduct);
                    await _eMDbContext.SaveChangesAsync();
                    responseDto.Success = false;
                    responseDto.Message = "product updated Successfully";
                }
                else
                {
                    responseDto.Success = true;
                    responseDto.Message = "product not Found";
                }
                return responseDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<ResponseDto<ProductDto>> Delete(int productId)
        {
            try
            {
                Product updateProduct = await _eMDbContext.Products.FirstOrDefaultAsync(u => u.Id == productId);
                if (updateProduct != null)
                {
                    updateProduct.DeletedDate = System.DateTime.Now;
                    updateProduct.IsDeleted = true;
                    _eMDbContext.Products.Update(updateProduct);
                    await _eMDbContext.SaveChangesAsync();
                    responseDto.Success = true;
                    responseDto.Message = "product Deleted Successfully";
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Message = "product not Found";
                }
                return responseDto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<ResponseDto<ProductDto>> GetById(int productId)
        {
            try
            {
                Product _product = await _eMDbContext.Products.FirstOrDefaultAsync(u => u.Id == productId);
                if (_product != null)
                {
                    responseDto.Success = true;
                    responseDto.Data = _mapper.Map<ProductDto>(_product);
                }
                else
                {
                    responseDto.Success = false;
                    responseDto.Message = "product not Found";
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
