using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingStuff.Models
{
    public class Transaction
    {
        public int transactionID { get; set; }
        public int accountID { get; set; }
        public int receiverID { get; set; }
        public int amount { get; set; }
        public DateTime transactionDate { get; set; }
    }
}