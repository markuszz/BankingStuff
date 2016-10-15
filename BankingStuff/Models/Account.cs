using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingStuff.Models
{
    public class Account
    {
        public int accountID { get; set; }
        public long customerID { get; set; }
        public int balance { get; set; }

        public Transaction transactions { get; set; }

    }
}