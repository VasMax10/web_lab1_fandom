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
    public class CharactersController : Controller
    {
        private readonly FandomContext _context;

        public CharactersController(FandomContext context)
        {
            _context = context;
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Characters.ToListAsync());
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters
                .FirstOrDefaultAsync(m => m.ID == id);
            if (characters == null)
            {
                return NotFound();
            }

            return View(characters);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Gender,Birthyear,Photo,Status,Info")] Characters characters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(characters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(characters);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters.FindAsync(id);
            if (characters == null)
            {
                return NotFound();
            }
            return View(characters);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Gender,Birthyear,Photo,Status,Info")] Characters characters)
        {
            if (id != characters.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(characters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharactersExists(characters.ID))
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
            return View(characters);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters
                .FirstOrDefaultAsync(m => m.ID == id);
            if (characters == null)
            {
                return NotFound();
            }

            return View(characters);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var characters = await _context.Characters.FindAsync(id);
            _context.Characters.Remove(characters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharactersExists(int id)
        {
            return _context.Characters.Any(e => e.ID == id);
        }
    }
}
