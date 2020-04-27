using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trash_Collector3.Data;
using Trash_Collector3.Models;

namespace TrashCollector2.Controllers
{

    [Authorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myCustomerProfile = _context.Customers.Where(q => q.IdentityUserId == userId).FirstOrDefault();
            var db = _context.Customers.Include(c => c.IdentityUser);
            return View(await db.ToListAsync());
            //if (myCustomerProfile == null)
            //{
            //    return RedirectToAction("Create");
            //}
            //else
            //{
            //    return View(myCustomerProfile);
            //}
        }

        // GET: Customers/Details/5
        public ActionResult Details()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myCustomerProfile = _context.Customers.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if (myCustomerProfile == null)
            {
                return NotFound();
            }


            return View(myCustomerProfile);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Address,ZipCode,PickUpDay,OneTimePickUp,StartOfSuspension,EndOfSupspension,Balance,IdentityUserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                customer.IdentityUserId = userId;
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myCustomerProfile = _context.Customers.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if (myCustomerProfile == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", myCustomerProfile.IdentityUserId);
            return View(myCustomerProfile);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,ZipCode,DayToPickUp,OneTimePickUp,StartOfSuspension,EndOfSupspension,Balance,IdentityUserId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myCustomerProfile = _context.Customers.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if (myCustomerProfile == null)
            {
                return NotFound();
            }

            return View(myCustomerProfile);

        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
