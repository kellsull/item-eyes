using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItemEyes.Data;
using ItemEyes.Models;
using Microsoft.AspNetCore.Authorization;

namespace ItemEyes.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemContext _context;

        public ItemsController(ItemContext context)
        {
            _context = context;
        }

        // GET: Items
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var itemContext = _context.Items
                .Include(i => i.Location)
                .OrderByDescending(i => i.ReceivedOn);
            return View(await itemContext.ToListAsync());
        }

        // GET: Items/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Inventory/5
        [Authorize]
        public async Task<IActionResult> Inventory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Where(i => i.LocationId == id)
                .Include(i => i.Location)
                .ToListAsync();

            return View(item);
        }

        // GET: Items/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantity,ReceivedOn,StorageZone,LocationId")] Item item)
        {
            GenerateProductId(item);
            item.OrderCount = item.Quantity;

            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", item.LocationId);
            return View(item);
        }

        // GET: Items/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", item.LocationId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Quantity,OrderCount,ReceivedOn,StorageZone,LocationId")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            GenerateProductId(item);

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = await _context.Items
                        .Include(i => i.Location)
                        .FirstOrDefaultAsync(m => m.Id == item.Id);
                    _context.Entry(existing).CurrentValues.SetValues(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", item.LocationId);
            return View(item);
        }

        // GET: Items/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        private async void GenerateProductId(Item item)
        {


            var items = await _context.Items
                .Include(i => i.Location)
                .OrderByDescending(i => i.ProductId)
                .ToListAsync();

            List<string> names = items.Select(i => i.Name).ToList();

            if (names.Contains(item.Name))
            {
                item.ProductId = items.Find(i => i.Name == item.Name).ProductId;
            }
            else
            {
                item.ProductId = items.First().ProductId + 1;
            }
        }
    }
}
