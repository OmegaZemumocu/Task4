using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task4.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace Task4.Controllers
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        private readonly ProductsContext _context;
        public ProductsController(ILogger<ProductsContext> logger, ProductsContext context)
        {
            _context = context;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            return View(products);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Quantity")] Product product)
        {
            //Console.WriteLine("Нулевой");
            if (id != product.Id)
            {
                //Console.WriteLine($"Первый: {id} и {product.Id}");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Console.WriteLine($"Второй: {id} и {product.Id} и {product.Name}");
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            return View(product);
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, [Bind("Id,Name,Price,Quantity")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Quantity")] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
