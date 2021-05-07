using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItemEyes.Data;
using ItemEyes.Models;
using Microsoft.AspNetCore.Routing;

namespace ItemEyes.Controllers
{
    [Route("api/Items")]
    [ApiController]
    public class ItemsApiController : ControllerBase
    {
        private readonly ItemContext _context;

        public ItemsApiController(ItemContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = await _context.Items
                .Include(i => i.Location)
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/Items/ProductId/productId
        [HttpGet("ProductId/{productId}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByProductId(int productId)
        {
            var items = await _context.Items
                .Include(i => i.Location)
                .Where(item => item.ProductId == productId)
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/Items/Name/name
        [HttpGet("Name/{name}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByName(string name)
        {
            var items = await _context.Items
                .Include(i => i.Location)
                .Where(item => item.Name.Contains(name))
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/Items/Location/locationId
        [HttpGet("Location/{locationId}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByLocation(int locationId)
        {
            var items = await _context.Items
                .Include(i => i.Location)
                .Where(item => item.LocationId == locationId)
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items
                .Include(i => i.Location)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {

            if (item == null) return BadRequest();
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // Records new items taken into inventory, should only be used items with the same location
        // POST: api/Items/locationId/Receive
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Receive/{locationId}")]
        public async Task<ActionResult<IEnumerable<Item>>> PostReceivedItems(int locationId, IEnumerable<Item> items)
        {
            foreach (Item item in items)
            {
                _context.Items.Add(item);
            }
            await _context.SaveChangesAsync();

            return Ok(items);
        }

        // Called to find which inventory items match requested product amounts, returning items to be
        // processed for fullfillment. Doesn't explicitly notify in case of insufficient stock.
        // This method ideally should be overloaded for different business strategies based of item 
        // usage paradigmns, specifically FIFO/FILO, closest available, most in stock, etc...
        // I've set it up to do FIFO for now.
        // This doesn't actually make the updates for sent items, it only returns their info
        // Get: api/Items/productId/quantity
        [HttpGet("{productId}/{quantity}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsToSend(int productId, int quantity )
        {
            var items = await _context.Items
                .Include(i => i.Location)
                .Where(item => item.ProductId == productId)
                .OrderBy(item => item.Quantity)
                .ToListAsync();

            List<Item> itemsToSend = new List<Item>();

            if ( items.Count() == 0 )
            {
                return BadRequest();
            }
            else
            {
                var count = quantity;
                foreach(Item item in items)
                {
                   if( count - item.Quantity > 0 )
                    {
                        itemsToSend.Add(item);
                        count = count - item.Quantity;
                    }
                    else
                    {
                        itemsToSend.Add(item);
                        break;
                    }
                }
            }

            return Ok(itemsToSend);
        }


        // This method decrements the stock quantity of an item by the quantity given to be sent
        // Get: api/Items/id/quantity/Send
        [HttpGet("Send/{id}/{quantity}")]
        public async Task<ActionResult<IEnumerable<Item>>> SendItems(int id, int quantity)
        {
            var item = await _context.Items
                .Include(i => i.Location)
                .FirstOrDefaultAsync(i => i.Id == id);

            if ((item.Quantity - quantity < 0) || (quantity <= 0))
            {
                return BadRequest();
            }
            else
            {
                item.Quantity = item.Quantity - quantity;
                await _context.SaveChangesAsync();

                if (item.Quantity == 0)
                {
                    _context.Items.Remove(item);
                    await _context.SaveChangesAsync();
                    return Ok(NoContent());
                }
                else 
                {
                    return Ok(item);
                }
            }
        }

        // Under the design model of this project, Delete should only be used when an items quantity decrements to zero
        // and would rarely if ever be called manually
        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(NoContent());
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        [Produces("application/json")]
        [HttpGet("Search/Name")]
        public async Task<IActionResult> SearchName()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var items = await _context.Items.ToListAsync();
                var names = items.Where(i => i.Name.Contains(term)).Select(i => i.Name).Distinct().ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
