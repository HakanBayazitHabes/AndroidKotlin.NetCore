using AndroidKotlin.API.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndroidKotlin.API.Controllers
{

    public class ProductsController : ODataController
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

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

    }
}
