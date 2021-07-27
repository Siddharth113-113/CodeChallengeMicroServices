using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Data;
using ProductServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _db;

        public ProductController(ProductContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Product> getProduct()
        {
            return _db.Product;
        }

        [HttpGet("{id}")]
        public IActionResult getProductById([FromRoute] int id)
        {
            var response = _db.Product.Where(m => m.productId == id).ToList();
            return Ok(response);

        }

        [HttpGet("{name}")]
        public IActionResult getProductByName([FromRoute] string name)
        {
            var response = _db.Product.Where(m => m.productName == name).ToList();
            return Ok(response);
        }

        [HttpGet("{category}")]
        public IActionResult getProductByCategory([FromRoute] string category)
        {
            var response = _db.Product.Where(m => m.Category == category).ToList();
            return Ok(response);
        }

        [HttpPost]
        public IActionResult createProduct([FromBody] Product product)
        {
            Product p = new Product()
            {
                productName = product.productName,
                Category = product.Category,
                Price = product.Price,
                Stock = product.Stock

            };
            // employee.EmployeeId = 14;
            _db.Product.Add(p);
            _db.SaveChanges();
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult putProduct(int id, Product product)
        {
            var existingProduct = _db.Product.Find(id);
            if (existingProduct != null)
            {
                existingProduct.productName = product.productName;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;

                _db.Product.Update(existingProduct);
                _db.SaveChanges();

                return Ok(existingProduct);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _db.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _db.Product.Remove(product);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
