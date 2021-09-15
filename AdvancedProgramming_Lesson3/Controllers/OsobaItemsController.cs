using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdvancedProgramming_Lesson3.Models;

namespace AdvancedProgramming_Lesson3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsobaItemsController : ControllerBase
    {
        private readonly OsobaContext _context;

        public OsobaItemsController(OsobaContext context)
        {
            _context = context;
        }

        // GET: api/OsobaItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OsobaItem>>> GetosobaItems()
        {
            return await _context.osobaItems.ToListAsync();
        }

        // GET: api/OsobaItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OsobaItem>> GetOsobaItem(long id)
        {
            var osobaItem = await _context.osobaItems.FindAsync(id);

            if (osobaItem == null)
            {
                return NotFound();
            }

            return osobaItem;
        }

        // PUT: api/OsobaItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOsobaItem(long id, OsobaItem osobaItem)
        {
            if (id != osobaItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(osobaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OsobaItemExists(id))
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

        // POST: api/OsobaItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OsobaItem>> PostOsobaItem(OsobaItem osobaItem)
        {
            _context.osobaItems.Add(osobaItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOsobaItem", new { id = osobaItem.Id }, osobaItem);
        }

        // DELETE: api/OsobaItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OsobaItem>> DeleteOsobaItem(long id)
        {
            var osobaItem = await _context.osobaItems.FindAsync(id);
            if (osobaItem == null)
            {
                return NotFound();
            }

            _context.osobaItems.Remove(osobaItem);
            await _context.SaveChangesAsync();

            return osobaItem;
        }

        private bool OsobaItemExists(long id)
        {
            return _context.osobaItems.Any(e => e.Id == id);
        }
    }
}
