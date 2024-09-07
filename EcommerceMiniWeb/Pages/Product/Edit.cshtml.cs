using EcommerceMini.Application.Dto;
using EcommerceMini.Application;
using EcommerceMini.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceMiniWeb.Pages.Product
{
    [Authorize]
    public class EditProductModel : PageModel
    {
        private readonly IProductAppService _productAppService;
        [BindProperty]
        public ProductDto productDto { get; set; }
        public EditProductModel(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _productAppService.GetById(id);
            productDto = response.Data;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _productAppService.Update(productDto);

            return RedirectToPage("./index");
        }
    }
}
