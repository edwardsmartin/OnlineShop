using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_side
{
    public class Orders
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Username { get; set; }

        public Orders(string productName, int quantity, string username)
        {
            this.ProductName = productName;
            this.Quantity = quantity;
            this.Username = username;
        }

    }
}
