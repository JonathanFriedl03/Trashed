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


namespace TrashCollector_3.Controllers
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
        public ActionResult Index()
        {
            var todayIs = DateTime.Now.DayOfWeek;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myEmployeeProfile = _context.Employees.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            var customerDb = _context.Customers.Where(f => (f.ZipCode == myEmployeeProfile.ZipCode && f.PickUpDay == todayIs)).ToList();

            if (myEmployeeProfile == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                EmployeeViewModel employeeViewModel = new EmployeeViewModel()
                {
                    Customers = customerDb,
                    Employee = myEmployeeProfile

                };

                employeeViewModel.PickupDay = new List<SelectListItem>();
                employeeViewModel.PickupDay.Add(new SelectListItem() { Text = "Monday", Value = "1"});
                employeeViewModel.PickupDay.Add(new SelectListItem() { Text = "Tuesdsay", Value = "2" });
                employeeViewModel.PickupDay.Add(new SelectListItem() { Text = "Wednesday", Value = "3" });
                employeeViewModel.PickupDay.Add(new SelectListItem() { Text = "Thursday", Value = "4" });
                employeeViewModel.PickupDay.Add(new SelectListItem() { Text = "Friday", Value = "5" });
                if (employeeViewModel.DaySelected == DayOfWeek.Sunday)
                {
                    employeeViewModel.DaySelected = DateTime.Now.DayOfWeek;
                    return View(employeeViewModel);
                }
                
                return View(employeeViewModel);
            }
        }

        [HttpPost]
        public ActionResult Index(EmployeeViewModel employeeViewModel)
        {
           // var todayIs = DateTime.Now.DayOfWeek;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myEmployeeProfile = _context.Employees.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            //var customerDb = _context.Customers.Where(c => (c.PickUpDay == employeeViewModel.DaySelected && c.ZipCode == )).ToList();
            var customerDb = _context.Customers.Where(f => (f.ZipCode == myEmployeeProfile.ZipCode && f.PickUpDay == employeeViewModel.DaySelected)).ToList();
            employeeViewModel.Customers = customerDb;
            employeeViewModel.Employee = myEmployeeProfile;
            return View(employeeViewModel);
            
        }


        // GET: Employees/Details/5
        public ActionResult Details(int id)
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
            Employee employee = new Employee();
            return View(employee);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,ZipCode,IdentityUserId")]Employee employee)
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
        public ActionResult Edit(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myEmployeeAccount = _context.Employees.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            //if (myEmployeeAccount == null)
            //{
            //    return NotFound();
            //}

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
        public ActionResult Delete(int id)
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
        public  IActionResult ConfirmPickup(int id)
        {
            var customerToPickup = _context.Customers.Where(a => a.Id == id).FirstOrDefault();
            customerToPickup.Balance += 25;
            _context.SaveChanges();
            //var newCustomerList = _context.Customers.Remove(customerToPickup);
            return RedirectToAction("Index post");

        }
    }
}
