using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BankingStuff.DataAccessLayer;
using BankingStuff.Models;

namespace BankingStuff.DataAccessLayer
{
    public class BankInitializer : System.Data.Entity.DropCreateDatabaseAlways<BankContext>
    {
        protected override void Seed(BankContext context)
        {
            var customers = new List<Customer>
            {
                new Customer{customerID=23069431151,password="pass",firstName="Markus", lastName="Hellestveit"},
                new Customer{customerID=12121212121,password="jew", firstName="johnny", lastName="lam"},
                new Customer{customerID=34343434343,password="ponny", firstName="per", lastName="Andreasen"},
                new Customer{ customerID=955940321951, password="matpåflaske", firstName="donald", lastName="trump"}
            };
            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();

            var accounts = new List<Account>
            {
                new Account{accountID=1,accountName="Brukskonto",customerID=23069431151,balance=666},
                new Account{accountID=2 ,accountName="Min Konto", customerID=12121212121,balance = 80000},
                new Account{accountID=3, accountName="Kontoen min", customerID=34343434343, balance=250},
                new Account{ accountID=4, accountName="Sparekonto", customerID=23069431151, balance=20000},

            };
            accounts.ForEach(s => context.Accounts.Add(s));
            context.SaveChanges();

            var transactions = new List<Transaction>
            {
                /*
                new Transaction{transactionID=1,accountID=1, receiverAccountID=2, transactionDate = DateTime.Parse("2016-11-12") },
                new Transaction{transactionID=2,accountID=2, receiverAccountID=3, transactionDate = DateTime.Parse("2016-09-12") },
                new Transaction{transactionID=3,accountID=3, receiverAccountID=1, transactionDate = DateTime.Parse("2016-01-12") },
                */
            };

            transactions.ForEach(s => context.Transactions.Add(s));
            context.SaveChanges();
        }
    }
}