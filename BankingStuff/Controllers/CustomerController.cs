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
                        FormsAuthentication.SetAuthCookie(cust.customerID.ToString(), false);
                        return RedirectToAction("Index", "Accounts");
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

        private ActionResult View(object cust)
        {
            throw new NotImplementedException();
        }
    }
}