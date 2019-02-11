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

        // GET: Verification
        public async Task<IActionResult> Index()
        {
            return View(await _context.Verification.ToListAsync());
        }

        // GET: Verification/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verification = await _context.Verification
                .FirstOrDefaultAsync(m => m.VerificationId == id);
            if (verification == null)
            {
                return NotFound();
            }

            return View(verification);
        }

        // GET: Verification/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Verification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VerificationId,VerificationName,VerificationDesc")] Verification verification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(verification);
        }

        // GET: Verification/Edit/5
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

        // POST: Verification/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VerificationId,VerificationName,VerificationDesc")] Verification verification)
        {
            if (id != verification.VerificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verification);
                    await _context.SaveChangesAsync();
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
            return View(verification);
        }

        // GET: Verification/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verification = await _context.Verification
                .FirstOrDefaultAsync(m => m.VerificationId == id);
            if (verification == null)
            {
                return NotFound();
            }

            return View(verification);
        }

        // POST: Verification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verification = await _context.Verification.FindAsync(id);
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
