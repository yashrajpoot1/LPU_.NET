using Ecommerce_Inventory.Data;
using Ecommerce_Inventory.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Inventory.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index(string gender = "")
        {
            var products = ProductData.GetProducts();
            
            if (!string.IsNullOrEmpty(gender))
            {
                products = products.Where(p => p.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            ViewBag.SelectedGender = gender;
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = ProductData.GetProducts().FirstOrDefault(p => p.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }
    }
}
