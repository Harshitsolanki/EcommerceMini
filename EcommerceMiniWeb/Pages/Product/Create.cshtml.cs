using EcommerceMini.Application.Dto;
using EcommerceMini.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceMiniWeb.Pages.Product
{
    [Authorize]
    public class CreateProductModel : PageModel
    {
        private readonly IProductAppService _productAppService;
        [BindProperty]
        public ProductDto productDto { get; set; }
        public CreateProductModel(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _productAppService.Create(productDto);
            
            return RedirectToPage("./index");
        }
    }
}
