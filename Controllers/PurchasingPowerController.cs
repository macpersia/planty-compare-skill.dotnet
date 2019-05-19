using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using planty_compare_portal.Data;

namespace planty_compare_portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasingPowerController : ControllerBase
    {
        // private readonly MyDataContext _context;
        private readonly MyDbContext _context;

        // public PurchasingPowerController(MyDataContext context)
        public PurchasingPowerController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchasingPower
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchasingPower>>> GetPurchasingPower()
        {
            return await _context.PurchasingPower.ToListAsync();
        }

        // GET: api/PurchasingPower/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchasingPower>> GetPurchasingPower(int id)
        {
            var purchasingPower = await _context.PurchasingPower.FindAsync(id);

            if (purchasingPower == null)
            {
                return NotFound();
            }

            return purchasingPower;
        }

        // PUT: api/PurchasingPower/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchasingPower(int id, PurchasingPower purchasingPower)
        {
            if (id != purchasingPower.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchasingPower).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchasingPowerExists(id))
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

        // POST: api/PurchasingPower
        [HttpPost]
        public async Task<ActionResult<PurchasingPower>> PostPurchasingPower(PurchasingPower purchasingPower)
        {
            _context.PurchasingPower.Add(purchasingPower);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchasingPower", new { id = purchasingPower.Id }, purchasingPower);
        }

        // DELETE: api/PurchasingPower/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchasingPower>> DeletePurchasingPower(int id)
        {
            var purchasingPower = await _context.PurchasingPower.FindAsync(id);
            if (purchasingPower == null)
            {
                return NotFound();
            }

            _context.PurchasingPower.Remove(purchasingPower);
            await _context.SaveChangesAsync();

            return purchasingPower;
        }

        private bool PurchasingPowerExists(int id)
        {
            return _context.PurchasingPower.Any(e => e.Id == id);
        }
    }
}
