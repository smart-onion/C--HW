using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCHW1.Models;

namespace MVCHW1.Controllers
{
    public class ProductController : Controller
    {
        readonly ProductContext productContext;
        public ProductController(ProductContext productContext)
        {
            this.productContext = productContext;
        }
        public async Task<IActionResult> Index() 
        {
            var products = await productContext.Products.ToListAsync();
            return Json(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string description, double price)
        {
            await productContext.AddAsync(new Product { Name = name, Description = description, Price = price });
            await productContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Search(string name)
        {
            var result = await productContext.Products.Where(p => p.Name == name).ToListAsync();
            return Json(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await productContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            return Json(result);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await productContext.Products.FirstOrDefaultAsync(p=>p.Id == id);
            if (product != null)
            {
                productContext.Products.Remove(product);
                await productContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
