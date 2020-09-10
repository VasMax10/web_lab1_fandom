using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_lab1_fandom.Models;

namespace web_lab1_fandom.Controllers
{
    public class CastsController : Controller
    {
        private readonly FandomContext _context;

        public CastsController(FandomContext context)
        {
            _context = context;
        }

        // GET: Casts
        public async Task<IActionResult> Index()
        {
            var fandomContext = _context.Casts.Include(c => c.Actor).Include(c => c.Character).Include(c => c.Series);
            return View(await fandomContext.ToListAsync());
        }

        // GET: Casts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casts = await _context.Casts
                .Include(c => c.Actor)
                .Include(c => c.Character)
                .Include(c => c.Series)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (casts == null)
            {
                return NotFound();
            }

            return View(casts);
        }

        // GET: Casts/Create
        public IActionResult Create()
        {
            ViewData["ActorID"] = new SelectList(_context.Actors, "ID", "ID");
            ViewData["CharacterID"] = new SelectList(_context.Characters, "ID", "ID");
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "ID");
            return View();
        }

        // POST: Casts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SeriesID,ActorID,CharacterID,FirstAppereance,LastAppereance")] Casts casts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(casts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorID"] = new SelectList(_context.Actors, "ID", "ID", casts.ActorID);
            ViewData["CharacterID"] = new SelectList(_context.Characters, "ID", "ID", casts.CharacterID);
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "ID", casts.SeriesID);
            return View(casts);
        }

        // GET: Casts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casts = await _context.Casts.FindAsync(id);
            if (casts == null)
            {
                return NotFound();
            }
            ViewData["ActorID"] = new SelectList(_context.Actors, "ID", "ID", casts.ActorID);
            ViewData["CharacterID"] = new SelectList(_context.Characters, "ID", "ID", casts.CharacterID);
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "ID", casts.SeriesID);
            return View(casts);
        }

        // POST: Casts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SeriesID,ActorID,CharacterID,FirstAppereance,LastAppereance")] Casts casts)
        {
            if (id != casts.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(casts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CastsExists(casts.ID))
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
            ViewData["ActorID"] = new SelectList(_context.Actors, "ID", "ID", casts.ActorID);
            ViewData["CharacterID"] = new SelectList(_context.Characters, "ID", "ID", casts.CharacterID);
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "ID", casts.SeriesID);
            return View(casts);
        }

        // GET: Casts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casts = await _context.Casts
                .Include(c => c.Actor)
                .Include(c => c.Character)
                .Include(c => c.Series)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (casts == null)
            {
                return NotFound();
            }

            return View(casts);
        }

        // POST: Casts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var casts = await _context.Casts.FindAsync(id);
            _context.Casts.Remove(casts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CastsExists(int id)
        {
            return _context.Casts.Any(e => e.ID == id);
        }
    }
}
