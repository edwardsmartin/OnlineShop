using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_side
{
    public class Account
    {
        public string Username { get; set; }
        public int AccountNumber { get; set; }

        public Account(string username, int accountNumber)
        {
            this.Username = username;
            this.AccountNumber = accountNumber;
        }
    }
}
