using EcommerceMini.Application.Dto;
using EcommerceMini.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceMiniWeb.Pages.Product
{
    [Authorize]
    public class DeleteProductModel : PageModel
    {
        private readonly IProductAppService _productAppService;
        [BindProperty]
        public ProductDto productDto { get; set; }
        public DeleteProductModel(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            ProductDto product = new ProductDto();
            product.Id = id;
            productDto = product;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _productAppService.Delete(id);

            return RedirectToPage("./index");
        }
    }
}
