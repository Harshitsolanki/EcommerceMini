using EcommerceMini.Application;
using EcommerceMini.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceMiniWeb.Pages.Product
{
    [Authorize]
    public class ProductsModel : PageModel
    {
        private readonly IProductAppService _productAppService;
        public IList<ProductDto> Products { get; set; }
        public ProductsModel(IProductAppService productAppService)
        {
            _productAppService = productAppService;            
        }

        public void OnGet()
        {
             var ProductList = _productAppService.GetProducts();
            if (ProductList != null && ProductList.Result.Data != null)
            {
                Products = ProductList.Result.Data;
            }
        }
    }
}
