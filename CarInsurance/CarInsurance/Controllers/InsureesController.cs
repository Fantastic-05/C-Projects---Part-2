using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarInsurance.Data;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsureesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insurees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Insurees.ToListAsync());
        }

        // GET: Insurees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insurees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                insuree.Quote = CalculateQuote(insuree);

                _context.Add(insuree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuree);
        }

        public async Task<IActionResult> Admin()
        {
            var insurees = await _context.Insurees.ToListAsync();
            return View(insurees);
        }

        private decimal CalculateQuote(Insuree insuree)
        {
            decimal quote = 50;

            // AGE RULES
            int age = DateTime.Now.Year - insuree.DateOfBirth.Year;

            if (age <= 18)
            {
                quote += 100;
            }
            else if (age >= 19 && age <= 25)
            {
                quote += 50;
            }
            else if (age >= 26)
            {
                quote += 25;
            }

            // CAR YEAR
            if (insuree.CarYear < 2000)
            {
                quote += 25;
            }

            if (insuree.CarYear > 2015)
            {
                quote += 25;
            }

            // MAKE / MODEL
            if (insuree.CarMake.ToLower() == "porsche")
            {
                quote += 25;

                if (insuree.CarModel != null && insuree.CarModel.ToLower() == "911 carrera")
                {
                    quote += 25;
                }
            }

            // SPEEDING TICKETS
            quote += insuree.SpeedingTickets * 10;

            // DUI
            if (insuree.DUI)
            {
                quote *= 1.25m;
            }

            // COVERAGE
            if (insuree.CoverageType.ToLower() == "full")
            {
                quote *= 1.5m;
            }

            return quote;
        }
    }
}
