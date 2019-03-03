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
    public class VerificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VerificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.Verification.ToListAsync());

        public IActionResult Create() => View();
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VerificationId,VerificationName,VerificationDesc")] Verification verification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verification);
                await _context.SaveChangesAsync();
                TempData["message"] = $"Zapisano {verification.VerificationName}.";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = $"Uzupełnij poprawnie wszystkie wymagane dane";
            return View(verification);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verification = await _context.Verification.FindAsync(id);
            if (verification == null)
            {
                return NotFound();
            }
            return View(verification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VerificationId,VerificationName,VerificationDesc")] Verification verification)
        {
           if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verification);
                    await _context.SaveChangesAsync();
                    TempData["message"] = $"Zapisano {verification.VerificationName}.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerificationExists(verification.VerificationId))
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
            return View(verification);
        }

        public async Task<IActionResult> Delete(int? verificationId)
        {
            if (verificationId == null)
            {
                return NotFound();
            }

            var verification = await _context.Verification
                .FirstOrDefaultAsync(m => m.VerificationId == verificationId);
            if (verification == null)
            {
                return NotFound();
            }

            return View(verification);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int verificationId)
        {
            var verification = await _context.Verification.FindAsync(verificationId);
            _context.Verification.Remove(verification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VerificationExists(int id)
        {
            return _context.Verification.Any(e => e.VerificationId == id);
        }
    }
}
