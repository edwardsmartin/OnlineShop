using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_side
{
    public class ShopDataChange
    {
        public enum ChangeType { ADD, UPDATE, REMOVE };
        public ChangeType Change { get; }
        public Account Account { get; }
        public Products Products { get; }
        public Orders Orders { get; }

        public ShopDataChange(ChangeType Change, Account Account, Products Products, Orders Orders)
        {
            this.Change = Change;
            this.Account = Account;
            this.Products = Products;
            this.Orders = Orders;
        }
    }
}
