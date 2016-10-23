using BankingStuff.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingStuff.Models
{
    public class Transaction
    {
        private BankContext db = new BankContext();
        public int transactionID { get; set; }
        public int accountID { get; set; }
        public int receiverAccountID { get; set; }
        public int amount { get; set; }
        [DataType(DataType.Date)]
        public DateTime transactionDate { get; set; }

        public long getCustomerID(Transaction transaction)
        {

            Account a = db.Accounts.First(x => x.accountID == transaction.accountID);
            long custID = a.customerID;
            return custID;
        }

        public string[] getAccountNameOfTransactionParticipant(Transaction transaction)
        {
            string[] a = new string[2];

            a[0] = db.Accounts.First(x => x.accountID == transaction.accountID).accountName;
            a[1] = db.Accounts.First(x => x.accountID == transaction.receiverAccountID).accountName;

            return a;
        }

        public string getFullNameOfTransactionRecipient(Transaction transaction)
        {
            long custID = getCustomerID(transaction);
            string name = "";

            name += db.Customers.First(x => x.customerID == custID).firstName + " ";
            name += db.Customers.First(x => x.customerID == custID).lastName;

            return name;


        }


    }


    
}