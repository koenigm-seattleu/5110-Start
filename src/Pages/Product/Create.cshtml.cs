using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly JsonFileProductService ProductService;

        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        public IActionResult OnGet()
        {
            var newProduct = ProductService.CreateData();

            return RedirectToPage("/Product/Update", new { id = newProduct.Id });
        }
    }
}