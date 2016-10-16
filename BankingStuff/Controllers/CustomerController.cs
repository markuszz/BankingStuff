using BankingStuff.DataAccessLayer;
using BankingStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BankingStuff.Controllers
{
    public class CustomerController : Controller
    {
        public long customerID;

        private BankContext db = new BankContext();

        // GET: Account
        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<Account> a = db.Accounts.ToList();
            foreach (var lul in a)
            {

            }
            return View(db.Accounts.ToList());
        }


        // GET: Customer
        public ActionResult LogIn()
        {
            return View();   
        }

        [HttpPost]
        public ActionResult LogIn(Customer customer, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Customer cust = new Customer();
                string password = Customer.GetUserPassword(customer.customerID);

                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "The user login or password provided is incorrect.");
                else
                {
                    if (customer.password.Equals(password))
                    {
                        customerID = cust.customerID;
                        FormsAuthentication.SetAuthCookie(cust.customerID.ToString(), false);
                        return RedirectToAction("Index", "Customer");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The password provided is incorrect.");
                    }
                }
            }

            // If we got this far, so qmething failed, redisplay form
            return View();
        }

        // GET: Customers/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "customerID,password,firstName,lastName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        private ActionResult View(object cust)
        {
            throw new NotImplementedException();
        }
    }
}