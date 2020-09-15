using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_lab1_fandom.Models;

namespace web_lab1_fandom.Controllers
{
    public class CharactersController : Controller
    {
        private readonly FandomContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _env;

        [Obsolete]
        public CharactersController(FandomContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Characters
        public async Task<IActionResult> Index(int? id, string name, string backImg)
        {
            //if (id == null) return RedirectToAction("Series","Index");
            ViewBag.SeriesID = id;
            ViewBag.SeriesName = name;
            if (backImg != null)
                backImg = backImg.Replace('\\', '/');
            ViewBag.backImg = backImg;
            if (id == null || name == null)
            {
                var _fandomContext = _context.Casts.Include(c => c.Actor).Include(c => c.Character);
                return View(await _context.Characters.ToListAsync());
            }
            var fandomContext = _context.Characters.Where(c => c.SeriesID == id).Include(c => c.Series);
            return View(await fandomContext.ToListAsync());
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
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "Name");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Create(IFormFile Photo, [Bind("ID,Name,Gender,Birthyear,Status,Info,SeriesID")] Characters characters)
        {
            if (Photo != null && Photo.Length > 0)
            {
                var imagePath = @"\Upload\Images\Characters\";
                var uploadPath = _env.WebRootPath + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var uniqFileName = Guid.NewGuid().ToString();
                var filename = Path.GetFileName(uniqFileName + "." + Photo.FileName.Split(".")[1].ToLower());

                string fullPath = uploadPath + filename;

                imagePath = imagePath + @"\";

                var filePath = Path.Combine(imagePath, filename);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await Photo.CopyToAsync(fileStream);
                }
                characters.Photo = filePath;
            }
            if (ModelState.IsValid)
            {
                _context.Add(characters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "ID", characters.SeriesID);
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
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "Name", characters.SeriesID);
            return View(characters);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Edit(int id, IFormFile Photo, [Bind("ID,Name,Gender,Photo,Birthyear,Status,Info,SeriesID")] Characters characters)
        {
            if (id != characters.ID)
            {
                return NotFound();
            }
            if (Photo != null && Photo.Length > 0)
            {
                if (characters.Photo != null)
                {
                    var PhotoPath = _env.WebRootPath + characters.Photo;
                    if (System.IO.File.Exists(PhotoPath))
                    {
                        System.IO.File.Delete(PhotoPath);
                    }
                }
                var imagePath = @"\Upload\Images\Characters\";
                var uploadPath = _env.WebRootPath + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var uniqFileName = Guid.NewGuid().ToString();
                var filename = Path.GetFileName(uniqFileName + "." + Photo.FileName.Split(".")[1].ToLower());

                string fullPath = uploadPath + filename;

                imagePath = imagePath + @"\";

                var filePath = Path.Combine(imagePath, filename);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await Photo.CopyToAsync(fileStream);
                }
                characters.Photo = filePath;
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
            ViewData["SeriesID"] = new SelectList(_context.Series, "ID", "Name", characters.SeriesID);
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
        [Obsolete]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var characters = await _context.Characters.FindAsync(id);
            
            if (characters.Photo != null)
            {
                var PhotoPath = _env.WebRootPath + characters.Photo;
                if (System.IO.File.Exists(PhotoPath))
                {
                    System.IO.File.Delete(PhotoPath);
                }
            }
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
