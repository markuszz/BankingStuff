using BankingStuff.DataAccessLayer;
using BankingStuff.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BankingStuff.Controllers
{
    public class CustomerController : Controller
    {
        private long customerID;
        private BankContext db = new BankContext();

        // GET: Customers/Details/5
        [Authorize]
        public ActionResult Details(long? id)
        {
            Debug.WriteLine(Session["custID"]);
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null || !customer.customerID.Equals(Session["custID"]))
            {
                return HttpNotFound();
            }
         

                                    
            return View(customer);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Details(Customer customer) 
        {
            Debug.Write("receiverID: ");
            Debug.Write(customer.Account.SingleOrDefault().Transaction.SingleOrDefault().receiverID);
            return RedirectToAction("Details", new { id = Session["custID"] });
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
                this.customerID = customer.customerID;
                string password = Customer.GetUserPassword(customer.customerID);

                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "The user login or password provided is incorrect.");
                else
                {
                    if (customer.password.Equals(password))
                    { 
                        Session["custID"] = customer.customerID;
                        FormsAuthentication.SetAuthCookie(customer.customerID.ToString(), false);
                        return RedirectToAction("Details", "Customer", new { id = customer.customerID});
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

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Customer");
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

    }
}