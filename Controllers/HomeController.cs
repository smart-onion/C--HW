using hw8.Models;
using hw8.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hw8.Controllers
{
    public class HomeController : Controller
    {
        ProductService productService;
        public HomeController(ProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View(productService.GetProducts());
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var product = productService.GetProduct(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            productService.Add(product);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            productService.Update(product);
            return RedirectToAction(nameof(Index));
        }


    }
}
