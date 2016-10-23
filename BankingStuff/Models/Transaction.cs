using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingStuff.Models
{
    public class Transaction
    {
        public int transactionID { get; set; }
        public int accountID { get; set; }
        public int receiverAccountID { get; set; }
        public int amount { get; set; }
        [DataType(DataType.Date)]
        public DateTime transactionDate { get; set; }
    }
}