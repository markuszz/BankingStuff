using BankingStuff.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankingStuff.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Login ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long customerID { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public virtual ICollection<Account> Account { get; set; }


        public static string GetUserPassword(long loginName)
        {
            using (DataAccessLayer.BankContext db = new BankContext())
            {
                var user = db.Customers.Where(o => o.customerID.Equals(loginName));
                if (user.Any())
                    return user.FirstOrDefault().password;
                else
                    return string.Empty;
            }
        }
    }


}