using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Measuring_equipment.Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Measuring_equipment.Controllers
{
    [Authorize(Roles = "Administratorzy")]
    public class PlaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Places.Include(p => p.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlaceId,PlaceName,PlaceDesc,DepartmentId")] Place place)
        {
            if (ModelState.IsValid)
            {
                _context.Add(place);
                await _context.SaveChangesAsync();
                TempData["message"] = $"Zapisano {place.PlaceName}.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", place.DepartmentId).OrderBy(x=>x.Text);
            TempData["error"] = $"Uzupełnij poprawnie wszystkie wymagane dane";
            return View(place);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", place.DepartmentId);
            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PlaceId,PlaceName,PlaceDesc,DepartmentId")] Place place)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(place);
                    await _context.SaveChangesAsync();
                    TempData["message"] = $"Zapisano {place.PlaceName}.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(place.PlaceId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", place.DepartmentId);
            TempData["error"] = $"Uzupełnij poprawnie wszystkie wymagane dane";
            return View(place);
        }

        public async Task<IActionResult> Delete(int? placeId)
        {
            if (placeId == null)
            {
                return NotFound();
            }

            var place = await _context.Places
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.PlaceId == placeId);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int placeId)
        {
            try
            {
                var place = await _context.Places.FindAsync(placeId);
                _context.Places.Remove(place);
                await _context.SaveChangesAsync();
                TempData["message"] = $"Usunięto {place.PlaceName}.";
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceExists(int id)
        {
            return _context.Places.Any(e => e.PlaceId == id);
        }
    }
}
