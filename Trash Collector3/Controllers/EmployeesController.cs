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

    [Authorize(Roles = "Employee")]
    public class EmployeesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myEmployeeProfile = _context.Employees.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            var customersDb = _context.Customers.Include(f => f.IdentityUser).ToList();
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                Customers = customersDb,
                Employee = myEmployeeProfile
            };
            return View(myEmployeeProfile);

        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myEmployeeAccount = _context.Employees.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            if (myEmployeeAccount == null)
            {
                return NotFound();
            }

            return View(myEmployeeAccount);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                employee.IdentityUserId = userId;
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", employee.IdentityUserId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myEmployeeAccount = _context.Employees.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            if (myEmployeeAccount == null)
            {
                return NotFound();
            }

            return View(myEmployeeAccount);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,ZipCode,IdentityUserId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", employee.IdentityUserId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myEmployeeAccount = _context.Employees.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            if (myEmployeeAccount == null)
            {
                return NotFound();
            }

            return View(myEmployeeAccount);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
        public async Task<IActionResult> ConfirmPickup(int id)
        {
            var customerToPickup = _context.Customers.Where(a => a.Id == id).FirstOrDefault();
            customerToPickup.Balance += 25;
            _context.SaveChanges();
            //var newCustomerList = _context.Customers.Remove(customerToPickup);
            return RedirectToAction("Index");

        }
    }
}
