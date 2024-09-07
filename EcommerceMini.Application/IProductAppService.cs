using EcommerceMini.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceMini.Application
{
    public interface IProductAppService
    {
        public Task<ResponseDto<List<ProductDto>>> GetProducts();
        public Task<ResponseDto<ProductDto>> Create(ProductDto productDto);
        public Task<ResponseDto<ProductDto>> Update(ProductDto productDto);
        public Task<ResponseDto<ProductDto>> Delete(int productId);
        public Task<ResponseDto<ProductDto>> GetById(int productId);

    }
}
