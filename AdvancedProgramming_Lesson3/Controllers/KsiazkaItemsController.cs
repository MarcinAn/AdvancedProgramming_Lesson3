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
    public class KsiazkaItemsController : ControllerBase
    {
        private readonly KsiazkaContext _context;

        public KsiazkaItemsController(KsiazkaContext context)
        {
            _context = context;
        }

        // GET: api/KsiazkaItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KsiazkaItem>>> GetksiazkaItems()
        {
            return await _context.ksiazkaItems.ToListAsync();
        }

        // GET: api/KsiazkaItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KsiazkaItem>> GetKsiazkaItem(long id)
        {
            var ksiazkaItem = await _context.ksiazkaItems.FindAsync(id);

            if (ksiazkaItem == null)
            {
                return NotFound();
            }

            return ksiazkaItem;
        }

        // PUT: api/KsiazkaItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKsiazkaItem(long id, KsiazkaItem ksiazkaItem)
        {
            if (id != ksiazkaItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(ksiazkaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KsiazkaItemExists(id))
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

        // POST: api/KsiazkaItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<KsiazkaItem>> PostKsiazkaItem(KsiazkaItem ksiazkaItem)
        {
            _context.ksiazkaItems.Add(ksiazkaItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKsiazkaItem", new { id = ksiazkaItem.Id }, ksiazkaItem);
        }

        // DELETE: api/KsiazkaItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KsiazkaItem>> DeleteKsiazkaItem(long id)
        {
            var ksiazkaItem = await _context.ksiazkaItems.FindAsync(id);
            if (ksiazkaItem == null)
            {
                return NotFound();
            }

            _context.ksiazkaItems.Remove(ksiazkaItem);
            await _context.SaveChangesAsync();

            return ksiazkaItem;
        }

        private bool KsiazkaItemExists(long id)
        {
            return _context.ksiazkaItems.Any(e => e.Id == id);
        }
    }
}
