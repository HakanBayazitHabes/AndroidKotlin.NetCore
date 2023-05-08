using AndroidKotlin.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace AndroidKotlin.API.Controllers
{
    [Authorize]
    public class ProductsController : ODataController
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery(PageSize = 5)]
        //odata/products
        public IActionResult Get()
        {
            return Ok(_context.Products.AsQueryable());
        }

        //odata/products(1)
        public IActionResult Get([FromODataUri] int key)
        {
            return Ok(_context.Products.Where(x => x.Id == key));
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody]Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        //odata/Products(4)
        [HttpPut]
        public async Task<IActionResult> PutProduct([FromODataUri] int key, [FromBody] Product product)
        {
            product.Id = key;

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromODataUri] int key)
        {
            var product = await _context.Products.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
