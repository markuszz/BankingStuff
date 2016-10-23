using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BankingStuff.DataAccessLayer;
using BankingStuff.Models;
using System.Security.Cryptography;

namespace BankingStuff.DataAccessLayer
{
    public class BankInitializer : System.Data.Entity.DropCreateDatabaseAlways<BankContext>
    {
        protected override void Seed(BankContext context)
        {

            var algorithm = System.Security.Cryptography.SHA512.Create();
            string pass1 = System.Text.Encoding.Default.GetString(algorithm.ComputeHash(System.Text.Encoding.ASCII.GetBytes("pass")));
            string pass2 = System.Text.Encoding.Default.GetString(algorithm.ComputeHash(System.Text.Encoding.ASCII.GetBytes("passord")));
            string pass3 = System.Text.Encoding.Default.GetString(algorithm.ComputeHash(System.Text.Encoding.ASCII.GetBytes("ponny")));
            string pass4 = System.Text.Encoding.Default.GetString(algorithm.ComputeHash(System.Text.Encoding.ASCII.GetBytes("matpåflaske")));


            var customers = new List<Customer>
            {

                new Customer {customerID=23069431151,password=pass1,firstName="Markus", lastName="Hellestveit"},
                new Customer{customerID=12121212121,password=pass2, firstName="johnny", lastName="lam"},
                new Customer{customerID=34343434343,password=pass3, firstName="per", lastName="Andreasen"},
                new Customer{customerID=95594032195, password=pass4, firstName="donald", lastName="trump"}
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
                
                new Transaction{transactionID=1,accountID=1, receiverAccountID=2, amount=3000, transactionDate = DateTime.Parse("2016-11-12") },
                new Transaction{transactionID=2,accountID=2, receiverAccountID=3, amount=9000, transactionDate = DateTime.Parse("2016-09-12") },
                new Transaction{transactionID=3,accountID=3, receiverAccountID=1, amount=909090, transactionDate = DateTime.Parse("2016-01-12") },
                new Transaction{transactionID=4,accountID=1, receiverAccountID=2, amount=2591, transactionDate = DateTime.Parse("2016-11-10") },
                new Transaction{transactionID=5,accountID=1, receiverAccountID=2, amount=4912, transactionDate = DateTime.Parse("2016-11-11") },



            };

            transactions.ForEach(s => context.Transactions.Add(s));
            context.SaveChanges();
        }
    }
}