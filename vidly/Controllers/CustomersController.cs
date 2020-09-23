using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidly.Models;
using vidly.ViewModel;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;

namespace vidly.Controllers
{
    [Authorize(Roles = RoleNames.canManageMovies)]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }
        [Route("Customers/Edit/{id}")]
        public ActionResult Edit(int id)
        { 
            NewCustomerViewModel viewmodel = new NewCustomerViewModel
            {
                Customers = _context.Customers.ToList().SingleOrDefault(c => c.Id == id),
                MembershipType = _context.MembershipType.ToList()
            };

            if (viewmodel.Customers != null)
                return View("New",viewmodel);
            else
                return HttpNotFound();
        }
        public ActionResult New()
        {
            NewCustomerViewModel newCustomerViewModel = new NewCustomerViewModel
            {
                MembershipType = _context.MembershipType.ToList(),
                Customers = new Customers()
            };
            return View(newCustomerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewCustomerViewModel c)
        {
            NewCustomerViewModel viewmodel = new NewCustomerViewModel
            {
                Customers = c.Customers,
            };
            if (!ModelState.IsValid)
            {
                return View("New", viewmodel);
            }

            //to add new customer in database
            if (c.Customers.Id == 0)
            {
                _context.Customers.Add(c.Customers);
                _context.SaveChanges();
            }

            //to updated exisiting customer in database
            if (c.Customers.Id != 0)
            {
                var customerInDb = _context.Customers.SingleOrDefault(cc => cc.Id == c.Customers.Id);
                customerInDb.Name = c.Customers.Name;
                customerInDb.DateOfBirth = c.Customers.DateOfBirth;
                customerInDb.MembershipTypeId = c.Customers.MembershipTypeId;
                customerInDb.IsSubscribed = c.Customers.IsSubscribed;
                _context.SaveChanges();
            }

            return RedirectToAction("Index","Customers");
        }
    }
}