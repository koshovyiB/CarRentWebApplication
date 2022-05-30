using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentWebApplication;

namespace CarRentWebApplication.Controllers
{
    public class CarsController : Controller
    {
        private readonly DBCarRentContext _context;

        public CarsController(DBCarRentContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Colors", "Index");
            //знаходження машин за кольором
            ViewBag.ColorId = id;
            ViewBag.ColorName = name;
            var carsByColor = _context.Cars.Where(c => c.ColorId == id).Include(c => c.Color);

            return View(await carsByColor.ToListAsync());
        }
       
        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Color)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create(int colorId)
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id");
            //ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id");
            ViewBag.ColorId = colorId;
            ViewBag.ColorName = _context.Colors.Where(c => c.Id == colorId).FirstOrDefault().Name;
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int colorId, [Bind("Id,Model,Year,DailyPrice,ColorId,BrandId")] Car car)
        {
            car.ColorId = colorId;
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
               // return RedirectToAction(nameof(Index));
               return RedirectToAction("Index", "Cars", new { id = colorId, name = _context.Colors.Where(c => c.Id == colorId).FirstOrDefault().Name});
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", car.BrandId);
            //ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", car.ColorId);
            //return View(car);
            return RedirectToAction("Index", "Cars", new { id = colorId, name = _context.Colors.Where(c => c.Id == colorId).FirstOrDefault().Name });
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", car.BrandId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", car.ColorId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,Year,DailyPrice,ColorId,BrandId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", car.BrandId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", car.ColorId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Color)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'DBCarRentContext.Cars'  is null.");
            }
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
          return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
