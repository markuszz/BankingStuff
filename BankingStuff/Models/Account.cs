using BankingStuff.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankingStuff.Models
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int accountID { get; set; }
        public string accountName { get; set; }
        [RegularExpression(@"[0-9]{11}", ErrorMessage = "Please write a correct person number")]
        public long customerID { get; set; }
        public int balance { get; set; }

        public string getAccount(long customerID)
        {
            BankContext db = new BankContext();
            string name = "";
            name += db.Customers.First(x => x.customerID == customerID).firstName;
            name += " " + db.Customers.First(x => x.customerID == customerID).lastName;

            return name;
        }

        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}