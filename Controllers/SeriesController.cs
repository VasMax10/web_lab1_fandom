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
    public class SeriesController : Controller
    {
        private readonly FandomContext _context;

        public SeriesController(FandomContext context)
        {
            _context = context;
        }

        // GET: Series
        public async Task<IActionResult> Index()
        {
            return View(await _context.Series.ToListAsync());
        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series
                .FirstOrDefaultAsync(m => m.ID == id);
            if (series == null)
            {
                return NotFound();
            }

            //return View(series);
            return RedirectToAction("Index", "Casts", new { id = series.ID, name = series.Name });
        }

        // GET: Series/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Poster,Premiere,IsEnded,BackImage,MainColor,SecondColor,Info")] Series series)
        {
            if (ModelState.IsValid)
            {
                _context.Add(series);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(series);
        }

        // GET: Series/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series.FindAsync(id);
            if (series == null)
            {
                return NotFound();
            }
            return View(series);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Poster,Premiere,IsEnded,BackImage,MainColor,SecondColor,Info")] Series series)
        {
            if (id != series.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(series);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeriesExists(series.ID))
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
            return View(series);
        }

        // GET: Series/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series
                .FirstOrDefaultAsync(m => m.ID == id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var series = await _context.Series.FindAsync(id);
            _context.Series.Remove(series);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeriesExists(int id)
        {
            return _context.Series.Any(e => e.ID == id);
        }
    }
}
