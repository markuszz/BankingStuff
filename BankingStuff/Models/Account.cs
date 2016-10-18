using System;
using System.Collections.Generic;
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
        public long customerID { get; set; }
        public int balance { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }

    }
}