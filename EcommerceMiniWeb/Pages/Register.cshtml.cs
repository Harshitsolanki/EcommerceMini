using EcommerceMini.Application.Dto;
using EcommerceMini.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EcommerceMiniWeb.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserAppService _userAppService;
        private readonly ILogger<RegisterModel> _logger;
        [BindProperty]
        public RegisterDto registerDto { get; set; }
        public RegisterModel(IUserAppService userAppService, ILogger<RegisterModel> logger)
        {
            _userAppService = userAppService;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _userAppService.CreateUser(registerDto);
                TempData["Message"] = "User registered Successfully Please login!!";
                return RedirectToPage("./Login");
            }
            else
            {
                return RedirectToPage("./Register");
            }
        }
    }

}
