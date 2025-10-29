using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class ReadModel : PageModel
    {
        private readonly JsonFileProductService ProductService;

        public ReadModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        public ProductModel Product { get; private set; }

        public IActionResult OnGet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage("/Product/Index");
            }

            Product = ProductService.GetAllData().FirstOrDefault(x => x.Id == id);

            if (Product == null)
            {
                return RedirectToPage("/Product/Index");
            }

            return Page();
        }
    }
}