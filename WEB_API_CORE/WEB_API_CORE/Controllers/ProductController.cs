using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WEB_API_CORE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly APIDbContext _context;

        public ProductController(APIDbContext context) 
        { 
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product pr)
        {
            var pro = new Product()
            {                
                Name = pr.Name,
                Code = pr.Code,
                Status = true
            };
            await _context.Products.AddAsync(pro);
            await _context.SaveChangesAsync();
            return Ok(pro);
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetProduct([FromRoute] long id)
        {
            var pr = await _context.Products.FindAsync(id);
            if (pr == null)
            {
                return NotFound();
            }
            return Ok(pr);
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> UpdateContact([FromRoute] long id, Product upPr)
        {
            var pr = await _context.Products.FindAsync(id);
            if (pr != null)
            {
                pr.Name = upPr.Name;
                pr.Code = upPr.Code;
                await _context.SaveChangesAsync();
                return Ok(pr);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> DeleteContact(long id)
        {
            var pr = await _context.Products.FindAsync(id);
            if (pr != null)
            {
                _context.Remove(pr);
                _context.SaveChanges();
                return Ok(pr);
            }
            return NotFound();
        }

    }
}
