using Microsoft.AspNetCore.Mvc;
using MVC_Assignment.Models;

namespace MVC_Assignment.Controllers
{
    public class ProductController : Controller
    {

        public static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 50000 },
            new Product { Id = 2, Name = "Phone", Price = 20000 },
            new Product { Id = 3, Name = "Headphones", Price = 2000 }
        };
        public IActionResult Index()
        {
            return View(products);
        }
    }
}
