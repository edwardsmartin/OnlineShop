using System;
using System.Collections.Concurrent;

namespace Client_side
{
    public interface IShopData
    {
        string HostName { get; set; }
        int HostPort { get; set; }
        int AccountNumber { get; set; }
        ConcurrentDictionary<int, Products> m_currentProducts { get; set; }
        ConcurrentDictionary<int, Orders> m_currentOrders { get; set; }
        ConcurrentDictionary<int, Account> m_currentAccounts {get; set;}

        void Disconnect();
        void Connect(int account_no);
        void Get_Products();
        void Get_Orders();
        void Purchase(string product_name);
    }
}