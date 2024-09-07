using EcommerceMini.Application.Dto;
using EcommerceMini.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceMiniAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IProductAppService _productAppService;
        private readonly ILogger<ProductsController> _logger;
        private ResponseDto<ProductDto> _responseDto;
        private ResponseDto<List<ProductDto>> _responseDtoList;
        public ProductsController(IProductAppService productAppService)
        {
            _productAppService  = productAppService;
            _responseDto = new ResponseDto<ProductDto>();
            _responseDtoList = new ResponseDto<List<ProductDto>>();
        }
        
        [HttpGet]
        public async Task<ResponseDto<List<ProductDto>>> GetAll()
        {
            try
            {
                return await _productAppService.GetProducts();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Get products");
                _responseDtoList.Success = false;
                _responseDtoList.Message = ex.Message;
                return _responseDtoList;
            }

        }

        [HttpGet("{productId}")]
        public async Task<ResponseDto<ProductDto>> GetById(int productId)
        {
            try
            {
                return await _productAppService.GetById(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error GetById product");
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }
        [HttpPost]
        public async Task<ResponseDto<ProductDto>> Register(ProductDto ProductDto)
        {
            try
            {
                return await _productAppService.Create(ProductDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Create product");
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }

        [HttpPut("{productId}")]
        public async Task<ResponseDto<ProductDto>> Update(int productId, ProductDto ProductDto)
        {
            try
            {
                if (productId != null && productId != 0)
                {
                    return await _productAppService.Update(ProductDto);
                }
                else
                {
                    _responseDto.Success = false;
                    _responseDto.Message = "ProductId not passed";
                    return _responseDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product");
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }


        [HttpDelete("{productId}")]
        public async Task<ResponseDto<ProductDto>> Delete(int productId)
        {
            try
            {
                return await _productAppService.Delete(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error delete product");
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }


    }
}
