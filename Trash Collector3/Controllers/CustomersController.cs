﻿using System;
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
        public ActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myCustomerProfile = _context.Customers.Where(q => q.IdentityUserId == userId).SingleOrDefault();
            //var db = _context.Customers.Include(c => c.IdentityUser);
            //return View(myCustomerProfile);
            if (myCustomerProfile == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return View(myCustomerProfile);
            }
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
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
            Customer customer = new Customer();
            return View(customer);
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
                customer.Balance = 0;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myCustomerProfile = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            //if (myCustomerProfile == null)
            //{
            //    return NotFound();
            //}
            //ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", myCustomerProfile.IdentityUserId);
            return View(myCustomerProfile);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,FirstName,LastName,Address,ZipCode,PickUpDay,OneTimePickUp,StartOfSuspension,EndOfSupspension,Balance,IdentityUserId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Customer customerEdit = _context.Customers.Where(c => c.Id == id).FirstOrDefault();
                    customerEdit.FirstName = customer.FirstName;
                    customerEdit.LastName = customer.LastName;
                    customerEdit.Address = customer.LastName;
                    customerEdit.ZipCode = customer.ZipCode;
                    customerEdit.PickUpDay = customer.PickUpDay;
                    customerEdit.OneTimePickUp = customer.OneTimePickUp;
                    customerEdit.StartOfSuspension = customer.StartOfSuspension;
                    customerEdit.EndOfSupspension = customer.EndOfSupspension;

                   // _context.Update(customer);
                    _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            Customer customer = _context.Customers.Where(c => c.Id == id).FirstOrDefault();

            return View(customer);

        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Customer customer)
        {
            var customer1 =  _context.Customers.Where(c => c.Id == id).FirstOrDefault();
            _context.Customers.Remove(customer1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}