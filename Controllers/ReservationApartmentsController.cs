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
    public class ReservationApartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public ReservationApartmentsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ReservationApartments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReservationsApartments.Include(r => r.Apartments).Include(r => r.Users);
            if (User.IsInRole("Admin"))
            {
                var reservationApartment = _context.ReservationsApartments
                                    .Include(o => o.Users)
                                    .Include(o => o.Apartments);
                return View(await reservationApartment.ToListAsync());
            }
            else
            {
                var reservationApartment = _context.ReservationsApartments
                                    .Include(o => o.Users)
                                    .Include(o => o.Apartments)
                                    .Where(x => x.UserId == _userManager.GetUserId(User));
                return View(await reservationApartment.ToListAsync());
            }
            
        }

        // GET: ReservationApartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reservationApartment = await _context.ReservationsApartments
               .Include(o => o.Users)
               .Include(o => o.Apartments)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationApartment == null)
            {
                return NotFound();
            }

            return View(reservationApartment);
        }
           
        

        // GET: ReservationApartments/Create
        public IActionResult Create(int apartmentId)
        {
            //ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "NameApartments");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: ReservationApartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentId,Comment,DateChoice")] ReservationApartment reservationApartment)
        {
            reservationApartment.DateReview=DateTime.Now;
            reservationApartment.UserId=_userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                _context.Add(reservationApartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "NameApartments", reservationApartment.ApartmentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", reservationApartment.UserId);
            return View(reservationApartment);
        }

        // GET: ReservationApartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationApartment = await _context.ReservationsApartments.FindAsync(id);
            if (reservationApartment == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "NameApartments", reservationApartment.ApartmentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", reservationApartment.UserId);
            return View(reservationApartment);
        }

        // POST: ReservationApartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ApartmentId,Comment,DateReview,DateChoice")] ReservationApartment reservationApartment)
        {
            if (id != reservationApartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationApartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationApartmentExists(reservationApartment.Id))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "NameApartments", reservationApartment.ApartmentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", reservationApartment.UserId);
            return View(reservationApartment);
        }

        // GET: ReservationApartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationApartment = await _context.ReservationsApartments
                .Include(r => r.Apartments)
                .Include(r => r.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationApartment == null)
            {
                return NotFound();
            }

            return View(reservationApartment);
        }

        // POST: ReservationApartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationApartment = await _context.ReservationsApartments.FindAsync(id);
            if (reservationApartment != null)
            {
                _context.ReservationsApartments.Remove(reservationApartment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationApartmentExists(int id)
        {
            return _context.ReservationsApartments.Any(e => e.Id == id);
        }
    }
}
