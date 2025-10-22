using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoApi.Data;
using DemoApi.Models;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoItemsController : ControllerBase
    {
        private readonly DemoContext _context;

        public DemoItemsController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/DemoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemoItem>>> GetDemoItems()
        {
            return await _context.DemoItems.ToListAsync();
        }

        // GET: api/DemoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DemoItem>> GetDemoItem(long id)
        {
            var demoItem = await _context.DemoItems.FindAsync(id);

            if (demoItem == null)
            {
                return NotFound();
            }

            return demoItem;
        }

        // PUT: api/DemoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDemoItem(long id, DemoItem demoItem)
        {
            if (id != demoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(demoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemoItemExists(id))
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

        // POST: api/DemoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DemoItem>> PostDemoItem(DemoItem demoItem)
        {
            _context.DemoItems.Add(demoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDemoItem", new { id = demoItem.Id }, demoItem);
        }

        // DELETE: api/DemoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemoItem(long id)
        {
            var demoItem = await _context.DemoItems.FindAsync(id);
            if (demoItem == null)
            {
                return NotFound();
            }

            _context.DemoItems.Remove(demoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DemoItemExists(long id)
        {
            return _context.DemoItems.Any(e => e.Id == id);
        }
    }
}
