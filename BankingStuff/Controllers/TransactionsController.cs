using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankingStuff.DataAccessLayer;
using BankingStuff.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BankingStuff.Controllers
{
    public class TransactionsController : Controller
    {
        private BankContext db = new BankContext();

        public PartialViewResult ShowAccounts(long receiverCustomerID)
        {
            var regex = @"[0-9]{11}";
            //string code = Request.Form["txtCode"];
            IEnumerable<Account> accounts = db.Accounts.Where(x => x.customerID == receiverCustomerID);
            var match = Regex.Match(receiverCustomerID.ToString(), regex, RegexOptions.IgnoreCase); 
            if (!match.Success)
            {
                string myStr = "Please write a correct person number";
                return PartialView((object)myStr);
            }
            return PartialView("_ShowAccounts", accounts);
        }

        public ActionResult showTransactions()
        {
            long custID = (long)Session["custID"];
            List<Account> allCustomerAccounts = db.Accounts.Where(x => x.customerID == custID).ToList();
            List<Transaction> allTransactions = new List<Transaction>();
            foreach(var item in allCustomerAccounts)
            {
                allTransactions.AddRange(item.Transaction.ToList());
                
            }
            return View(allTransactions);
        }


       

        // GET: Transactions
        public ActionResult Index()
        {
            return View(db.Transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        [Authorize]
        [HttpPost]
        public ActionResult MakeTransaction(int accountID)
        {
            ViewBag.accountID = accountID;
            
            return View(new Transaction());
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public ActionResult RegisterTransaction([Bind(Include = "transactionID,accountID,receiverAccountID,amount,transactionDate")] Transaction transaction)
        {
            // if (ModelState.IsValid)
            //  {
         
      
            db.Transactions.Add(transaction);
            db.SaveChanges();

                return RedirectToAction("Details", "Customer", new { id = Session["custID"]});
          //  }

            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "transactionID,accountID,receiverAccountID,amount,transactionDate")] Transaction transaction)
        {
            Transaction original = db.Transactions.First(x => x.transactionID == transaction.transactionID);
            original.receiverAccountID = transaction.receiverAccountID;
            original.amount = transaction.amount;
            original.transactionDate = transaction.transactionDate;


            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("ShowTransactions");
            }
            return View(transaction);
        }

  
        public ActionResult Delete(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("ShowTransactions");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
