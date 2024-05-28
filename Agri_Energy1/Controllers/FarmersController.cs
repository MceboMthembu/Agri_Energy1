using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agri_Energy1.Data;
using Agri_Energy1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;



namespace Agri_Energy1.Controllers

{
    [Authorize(Roles = "Employee")]
    public class FarmersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public FarmersController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Farmers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Farmers.ToListAsync());
        }

        // GET: Farmers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmers = await _context.Farmers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmers == null)
            {
                return NotFound();
            }

            return View(farmers);
        }

        // GET: Farmers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Farmers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password")] Farmers farmers)
        {
            if (ModelState.IsValid)
            {
                // Create IdentityUser and add to AspNetUsers
                var user = new IdentityUser
                {
                    UserName = farmers.Email,
                    Email = farmers.Email
                };

                var result = await _userManager.CreateAsync(user, farmers.Password);
                if (result.Succeeded)
                {
                    // Assign the role of Farmer
                    await _userManager.AddToRoleAsync(user, "Farmer");

                    // Add the farmer to the Farmers table
                    _context.Add(farmers);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the case where user creation failed
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(farmers);
        }
    

        // POST: Farmers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password")] Farmers farmers)
        {
            if (id != farmers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmersExists(farmers.Id))
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
            return View(farmers);
        }

        // GET: Farmers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmers = await _context.Farmers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmers == null)
            {
                return NotFound();
            }

            return View(farmers);
        }

        // POST: Farmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmers = await _context.Farmers.FindAsync(id);
            if (farmers != null)
            {
                _context.Farmers.Remove(farmers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmersExists(int id)
        {
            return _context.Farmers.Any(e => e.Id == id);
        }

        public IActionResult FilteredProducts(int id)
        {
            return RedirectToAction("FarmerProducts", "Products", new { id });
        }

        // GET: Farmers/FarmerProducts/5
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> FarmerProducts(int? id, string startDate = "", string endDate = "", string category = "")
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers.FirstOrDefaultAsync(m => m.Id == id);
            if (farmer == null)
            {
                return NotFound();
            }

            // Convert date strings to DateTime objects
            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MaxValue;

            if (!string.IsNullOrEmpty(startDate))
            {
                start = DateTime.Parse(startDate);
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                end = DateTime.Parse(endDate);
            }

            // Apply filters
            var filteredProducts = await _context.Products
                .Where(p => _context.FarmerProducts.Any(fp => fp.FarmerId == farmer.Id && fp.ProductId == p.Id))
                .Where(p => string.IsNullOrEmpty(category) ? true : p.Category == category)
                .Where(p => p.ProductionDate >= start && p.ProductionDate <= end)
                .ToListAsync();

            ViewBag.FarmerId = id; // Pass the farmer id to the view for the form action
            ViewBag.FarmerEmail = farmer.Email; // Pass the farmer's email to the view
            return View(filteredProducts);
        }



    }
}
