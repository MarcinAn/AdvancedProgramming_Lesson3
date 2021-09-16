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
        public async Task<ActionResult<IEnumerable<KsiazkaItemDTO>>> GetKsiazkaItems()
        {
            return await _context.ksiazkaItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/KsiazkaItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KsiazkaItemDTO>> GetKsiazkaItem(long id)
        {
            var ksiazkaItem = await _context.ksiazkaItems.FindAsync(id);
            if (ksiazkaItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(ksiazkaItem);
        }

        [HttpPost]
        [Route("UpdateKsiazkaItem")]
        public async Task<ActionResult<KsiazkaItemDTO>> UpdateKsiazkaItem(KsiazkaItemDTO ksiazkaItemDTO)
        {
            var ksiazkaItem = await _context.ksiazkaItems.FindAsync(ksiazkaItemDTO.Id);
            if (ksiazkaItem == null)
            {
                return NotFound();
            }
            ksiazkaItem.Title = ksiazkaItemDTO.Title;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!KsiazkaItemExists(ksiazkaItemDTO.Id))
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(UpdateKsiazkaItem),
                new { id = ksiazkaItem.Id },
                ItemToDTO(ksiazkaItem));
        }

        [HttpPost]
        [Route("CreateKsiazkaItem")]
        public async Task<ActionResult<KsiazkaItemDTO>> CreateKsiazkaItem(KsiazkaItemDTO ksiazkaItemDTO)
        {
            var ksiazkaItem = new KsiazkaItem
            {
                Title = ksiazkaItemDTO.Title,
            };

            _context.ksiazkaItems.Add(ksiazkaItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetKsiazkaItem),
                new { id = ksiazkaItem.Id },
                ItemToDTO(ksiazkaItem));
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
            return NoContent();
        }


        private bool KsiazkaItemExists(long id) =>
            _context.ksiazkaItems.Any(e => e.Id == id);

        private static KsiazkaItemDTO ItemToDTO(KsiazkaItem ksiazkaItem) =>
            new KsiazkaItemDTO
            {
                Id = ksiazkaItem.Id,
                Title = ksiazkaItem.Title,
            };
    }
}
