using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Measuring_equipment.Models;

namespace Measuring_equipment.Controllers
{
    public class ProducerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProducerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.Producers.ToListAsync());
        

        public IActionResult Create() => View();
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProducerId,ProducerName,ProducerDesc")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producer);
                await _context.SaveChangesAsync();
                TempData["message"] = $"Zapisano {producer.ProducerName}.";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = $"Uzupełnij poprawnie wszystkie wymagane dane";
            return View(producer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = await _context.Producers.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProducerId,ProducerName,ProducerDesc")] Producer producer)
        {
            if (id != producer.ProducerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producer);
                    await _context.SaveChangesAsync();
                    TempData["message"] = $"Zapisano {producer.ProducerName}.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerExists(producer.ProducerId))
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
            TempData["error"] = $"Uzupełnij poprawnie wszystkie wymagane dane";
            return View(producer);
        }

        public async Task<IActionResult> Delete(int? producerId)
        {
            if (producerId == null)
            {
                return NotFound();
            }

            var producer = await _context.Producers
                .FirstOrDefaultAsync(m => m.ProducerId == producerId);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int producerId)
        {
            try
            {
                var producer = await _context.Producers.FindAsync(producerId);
                _context.Producers.Remove(producer);
                await _context.SaveChangesAsync();
                TempData["message"] = $"Usunięto {producer.ProducerName}.";
            }
            catch
            {
                TempData["error"] = $"Nie można usunąć, istnieją typy powiązane z tym producentem";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
            return _context.Producers.Any(e => e.ProducerId == id);
        }
    }
}
