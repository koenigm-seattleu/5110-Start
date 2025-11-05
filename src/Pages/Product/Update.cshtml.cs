using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class UpdateModel : PageModel
    {
        private readonly JsonFileProductService ProductService;

        public UpdateModel(JsonFileProductService productService)
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

            Product = ProductService.GetAllData().FirstOrDefault(x => x.Id == id);
            if (Product == null)
            {
                return RedirectToPage("/Product/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            System.Console.WriteLine("OnPost called for Product: " + Product?.Id);

            if (Product == null)
            {
                return RedirectToPage("/Product/Index");
            }
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductService.UpdateData(Product);
            return RedirectToPage("/Product/Index");
        }
    }
}