using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_side
{
    public class Products
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public Products(string productName, int quantity)
        {
            this.ProductName = productName;
            this.Quantity = quantity;
        }
    }
}
