using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class DeleteModel : PageModel
    {
        private readonly JsonFileProductService ProductService;

        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        [BindProperty]
        public ProductModel Product { get; set; }

        public IActionResult OnGet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage("/Product/Index");
            }

            Product = ProductService.GetAllData().FirstOrDefault(p => p.Id == id);
            if (Product == null)
            {
                return RedirectToPage("/Product/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (Product?.Id == null)
            {
                return RedirectToPage("/Product/Index");
            }

            ProductService.DeleteData(Product.Id);
            return RedirectToPage("/Product/Index");
        }
    }
}