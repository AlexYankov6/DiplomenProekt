using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProektAleks.Data;

namespace ProektAleks.Controllers
{
    [Authorize]
    public class ReservationHousesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ReservationHousesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ReservationHouses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReservationsHouses.Include(r => r.Houses).Include(r => r.Users);
            if (User.IsInRole("Admin"))
            {
                var reservationHouse = _context.ReservationsHouses
                                    .Include(o => o.Users)
                                    .Include(o => o.Houses);
                return View(await reservationHouse.ToListAsync());
            }
            else
            {
                var reservationHouse = _context.ReservationsHouses
                                    .Include(o => o.Users)
                                    .Include(o => o.Houses)
                                    .Where(x => x.UserId == _userManager.GetUserId(User));
                return View(await reservationHouse.ToListAsync());
            }
        }

        // GET: ReservationHouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationHouse = await _context.ReservationsHouses
                .Include(r => r.Houses)
                .Include(r => r.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationHouse == null)
            {
                return NotFound();
            }

            return View(reservationHouse);
        }

        // GET: ReservationHouses/Create
        public IActionResult Create(int houseId)
        {
            //ViewData["HouseId"] = new SelectList(_context.Houses, "Id", "Name");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: ReservationHouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HouseId,Comment,DateChoice")] ReservationHouse reservationHouse)
        {
            reservationHouse.DateReview = DateTime.Now;
            reservationHouse.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                _context.Add(reservationHouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HouseId"] = new SelectList(_context.Houses, "Id", "NameProperty", reservationHouse.HouseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", reservationHouse.UserId);
            return View(reservationHouse);
        }

        // GET: ReservationHouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationHouse = await _context.ReservationsHouses.FindAsync(id);
            if (reservationHouse == null)
            {
                return NotFound();
            }
            ViewData["HouseId"] = new SelectList(_context.Houses, "Id", "NameProperty", reservationHouse.HouseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", reservationHouse.UserId);
            return View(reservationHouse);
        }

        // POST: ReservationHouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,HouseId,Comment,DateReview,DateChoice")] ReservationHouse reservationHouse)
        {
            if (id != reservationHouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationHouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationHouseExists(reservationHouse.Id))
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
            ViewData["HouseId"] = new SelectList(_context.Houses, "Id", "NameProperty", reservationHouse.HouseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", reservationHouse.UserId);
            return View(reservationHouse);
        }

        // GET: ReservationHouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationHouse = await _context.ReservationsHouses
                .Include(r => r.Houses)
                .Include(r => r.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationHouse == null)
            {
                return NotFound();
            }

            return View(reservationHouse);
        }

        // POST: ReservationHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationHouse = await _context.ReservationsHouses.FindAsync(id);
            if (reservationHouse != null)
            {
                _context.ReservationsHouses.Remove(reservationHouse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationHouseExists(int id)
        {
            return _context.ReservationsHouses.Any(e => e.Id == id);
        }
    }
}
