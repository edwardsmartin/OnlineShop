using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_side
{
    public class ShopClientHandler
    {
        private readonly TcpClient m_tcpClient;
        private readonly ConcurrentDictionary<int, Products> m_currentProducts;
        private readonly ConcurrentDictionary<int, Orders> m_currentOrders;
        private readonly ConcurrentDictionary<int, Account> m_currentAccount;   

        public ShopClientHandler(TcpClient tcpClient, ConcurrentDictionary<int, Products> currentProducts, ConcurrentDictionary<int, Orders> currentOrders, ConcurrentDictionary<int, Account> accounts)
        {
            m_tcpClient = tcpClient;
            m_currentProducts = currentProducts;
            m_currentOrders = currentOrders;
            m_currentAccount = accounts;
        }

        // Handle client session
        public void Run(object threadInfo)
        {
            // Using makes sure TcpClient is closed at end of scope
            using (m_tcpClient)
            {
                try
                {
                    NetworkStream stream = m_tcpClient.GetStream();
                    StreamReader reader = new StreamReader(stream);
                    StreamWriter writer = new StreamWriter(stream);
                    
                    string line = reader.ReadLine();
                    if (line != null) // A null value indicates the client may have closed the connection
                    {
                        string[] request = line.Split(':');
                        string cmd = request[0]; // Command comes before : Deals with CONNECT and DISCONNECT

                        if (2 == request.Length && int.TryParse(request[1], out int accountNumber)) // Account Number comes after : 
                        {
                            try
                            {
                                if ("CONNECT" == cmd)
                                {
                                    bool connected = false;

                                    Console.WriteLine(line);

                                    // Checks to see if the account number is valid

                                    foreach (var account in m_currentAccount)
                                    {
                                        if (account.Key == accountNumber)
                                        {
                                            writer.WriteLine($"CONNECTED:{account.Value.Username}");
                                            Console.WriteLine($"CONNECTED:{account.Value.Username}");
                                            m_currentOrders.TryAdd(m_currentOrders.Count, new Orders("", 0, account.Value.Username));
                                            writer.Flush();
                                            connected = true;
                                        }
                                    }   
                                    if (!connected)
                                    {
                                        writer.WriteLine($"CONNECT_ERROR");
                                        Console.WriteLine($"CONNECT_ERROR");
                                        writer.Flush();
                                    }
                                }
                            }
                            catch (InvalidOperationException)
                            {
                                // Product not found
                            }
                        }
                        else if ("PURCHASE" == cmd) // Product Name comes after : Deals with PURCHASE
                        {
                            string productName = request[1];
                            bool purchased = true;
                            string[] items = new string[] { "Apple", "Banana", "Cucumber", "Tomato", "Pear" };

                            Console.WriteLine($"PURCHASE:{productName}");

                            try
                            {
                                lock (m_currentOrders)
                                {
                                    if (!items.Contains(productName))
                                    {
                                        writer.WriteLine("NOT_VALID");
                                        Console.WriteLine("NOT_VALID");
                                        writer.Flush();

                                        purchased = false;
                                    }

                                    // Checks to see if user has ordered the product before
                                    foreach (var order in m_currentOrders)
                                    {
                                        foreach (var product in m_currentProducts)
                                        {
                                            if (purchased)
                                            {
                                                if (product.Value.ProductName == productName)   // checks to see if the product is in the list of available products
                                                {
                                                    string username = order.Value.Username;

                                                    if (product.Value.Quantity == 0)
                                                    {
                                                        writer.WriteLine("NOT_AVAILABLE");
                                                        Console.WriteLine("NOT_AVAILABLE");
                                                        writer.Flush();

                                                        purchased = false;
                                                    }
                                                    else
                                                    {
                                                        if (product.Value.ProductName == order.Value.ProductName)   // checks to see if the product has already been ordered
                                                        {
                                                            order.Value.Quantity += 1;

                                                            // THE ORDER SUCCESSFULLY PLACED
                                                            writer.WriteLine("DONE");
                                                            Console.WriteLine("DONE");
                                                            writer.Flush(); // Push out any text that is still in the buffer

                                                            if (product.Value.Quantity > 0)
                                                                product.Value.Quantity--;
                                                            purchased = false;                                      
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    // Checks to see if the user is purchasing the product for the first time
                                    foreach (var order in m_currentOrders)
                                    {
                                        foreach (var product in m_currentProducts)
                                        {
                                            if (purchased)
                                            {
                                                if (product.Value.ProductName == productName)   // checks to see if the product is in the list of available products
                                                {
                                                    string username = order.Value.Username;

                                                    if (product.Value.Quantity == 0)
                                                    {
                                                        writer.WriteLine("NOT_AVAILABLE");
                                                        Console.WriteLine("NOT_AVAILABLE");
                                                        writer.Flush();

                                                        purchased = false;
                                                    }
                                                    else
                                                    {
                                                        if (order.Value.Quantity == 0)
                                                        {
                                                            m_currentOrders.TryRemove(m_currentOrders.Count - 1, out _);
                                                        }
                                                        
                                                        m_currentOrders.TryAdd(m_currentOrders.Count, new Orders(product.Value.ProductName, 1, username));

                                                        // THE ORDER SUCCESSFULLY PLACED
                                                        writer.WriteLine("DONE");
                                                        Console.WriteLine("DONE");
                                                        writer.Flush(); // Push out any text that is still in the buffer

                                                        if (product.Value.Quantity > 0)
                                                            product.Value.Quantity--;
                                                        purchased = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (InvalidOperationException)
                            {
                                // The specified product is not available
                            }
                        }
                        else    // Deals with GET_PRODUCTS, GET_ORDERS
                        {
                            try
                            {
                                if (line == "GET_PRODUCTS")
                                {
                                    string products = "PRODUCTS:";

                                    Console.WriteLine("GET_PRODUCTS");

                                    foreach (var item in m_currentProducts)
                                    {
                                        products += $"{item.Value.ProductName},{item.Value.Quantity}|";
                                    }

                                    writer.WriteLine(products);
                                    Console.WriteLine(products);
                                    writer.Flush();
                                }
                                else if (line == "GET_ORDERS")
                                {
                                    string orders = "ORDERS:";

                                    Console.WriteLine("GET_ORDERS");

                                    foreach (var item in m_currentOrders)
                                    {
                                        if (item.Value.Quantity != 0)
                                            orders += $"{item.Value.ProductName},{item.Value.Quantity},{item.Value.Username}|";
                                    }

                                    writer.WriteLine(orders);
                                    Console.WriteLine(orders);
                                    writer.Flush();
                                }
                                if ("DISCONNECT" == cmd)
                                {
                                    Console.WriteLine("DISCONNECT");                                    
                                }
                            }
                            catch (InvalidOperationException)
                            {
                                // Product not found
                            }
                        }
                    }
                }
                catch (IOException) // Exception takes us out of the loop, so client handler thread will end
                {
                    Console.WriteLine("***Network Error***");
                }
                catch (OutOfMemoryException)
                {
                    // Catch buffer overflow exception
                    // Connection will close upon leaving the using block
                }
            }
        }
    }
}
