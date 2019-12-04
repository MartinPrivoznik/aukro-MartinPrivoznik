using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataManagement.Models;

namespace DataManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleItemsController : ControllerBase
    {
        private readonly AukroContext _context;

        public SaleItemsController()
        {
            _context = new AukroContext();
        }

        // GET: api/SaleItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleItem>>> GetSaleItem()
        {
            return await _context.SaleItem.ToListAsync();
        }

        // GET: api/SaleItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleItem>> GetSaleItem(int id)
        {
            var saleItem = await _context.SaleItem.FindAsync(id);

            if (saleItem == null)
            {
                return NotFound();
            }

            return saleItem;
        }

        // GET: api/SaleItems/byuserid?id='x'
        [HttpGet("byuserid")]
        public async Task<ActionResult<IEnumerable<SaleItem>>> GetSaleItemByUserID(int id)
        {
            var saleItems = _context.SaleItem.Where(usr => usr.UserId == id).ToArray();

            if (saleItems == null)
            {
                return NotFound();
            }

            return saleItems;
        }

        // GET: api/SaleItems/bynotuserid?id='x'
        [HttpGet("bynotuserid")]
        public async Task<ActionResult<IEnumerable<SaleItem>>> GetSaleItemByNotUserID(int id)
        {
            var saleItems = _context.SaleItem.Where(usr => usr.UserId != id).ToArray();

            if (saleItems == null)
            {
                return NotFound();
            }

            return saleItems;
        }

        // PUT: api/SaleItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleItem(int id, SaleItem saleItem)
        {
            if (id != saleItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(saleItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SaleItems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SaleItem>> PostSaleItem(SaleItem saleItem)
        {
            _context.SaleItem.Add(saleItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaleItem", new { id = saleItem.Id }, saleItem);
        }

        // DELETE: api/SaleItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SaleItem>> DeleteSaleItem(int id)
        {
            var saleItem = await _context.SaleItem.FindAsync(id);
            if (saleItem == null)
            {
                return NotFound();
            }

            _context.SaleItem.Remove(saleItem);
            await _context.SaveChangesAsync();

            return saleItem;
        }

        private bool SaleItemExists(int id)
        {
            return _context.SaleItem.Any(e => e.Id == id);
        }
    }
}
