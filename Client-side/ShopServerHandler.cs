using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client_side
{
    public class ShopServerHandler : IShopData
    {
        public string HostName { get; set; } = "localhost"; // Default host name - localhost means that the server is running on the same computer as this client
        public int HostPort { get; set; } = 55057; // Default host port - note that the port number is the same one as in the server code
        public int AccountNumber { get; set; }
        public ConcurrentDictionary<int, Orders> m_currentOrders { get; set; } = new ConcurrentDictionary<int, Orders>();
        public ConcurrentDictionary<int, Products> m_currentProducts { get; set; } = new ConcurrentDictionary<int, Products>();
        public ConcurrentDictionary<int, Account> m_currentAccounts { get; set; } = new ConcurrentDictionary<int, Account>();
        ShopClientForm shopClientForm = new ShopClientForm();
        SetHostForm setHostForm = new SetHostForm();

        // DISCONNECT
        public void Disconnect()
        {
            // To make sure DISCONNECT gets sent, use a foreground thread
            Thread thClose = new Thread(threadInfo =>
            {
                (string hostName, int hostPort) = ((string, int))threadInfo; // Deconstruct tuple
                try
                {
                    using (TcpClient tcpClient = new TcpClient()) // Default constructor only allows IPv4
                    {
                        tcpClient.Connect(hostName, hostPort);
                        NetworkStream stream = tcpClient.GetStream();
                        StreamWriter writer = new StreamWriter(stream);
                        writer.WriteLine("DISCONNECT");
                        writer.Flush();
                    }
                }
                catch (IOException)
                {
                    // Indicate server unavailable
                }
                catch (SocketException)
                {
                    // Indicate server unavailable
                }
            });
            thClose.Start((HostName, HostPort)); // Thread parameters (in tuple)
        }

        // CONNECT
        public void Connect(int account_no)
        {
            ThreadPool.QueueUserWorkItem(threadInfo =>
            {
                (string hostName, int hostPort) = ((string, int))threadInfo; // Deconstruct tuple
                try
                {
                    using (TcpClient tcpClient = new TcpClient()) // Default constructor only allows IPv4
                    {
                        tcpClient.Connect(hostName, hostPort);
                        NetworkStream stream = tcpClient.GetStream();
                        StreamReader reader = new StreamReader(stream);
                        StreamWriter writer = new StreamWriter(stream);
                        writer.WriteLine($"CONNECT:{account_no}");
                        writer.Flush();
                        string line = reader.ReadLine();
                        string[] request = line.Split(':');
                        if (line == "CONNECT_ERROR") // A null value indicates the client may have closed the connection
                        {
                            setHostForm.DisplayMessage("CONNECT_ERROR");                   
                        }
                        else if (request[0] == "CONNECTED")
                        {
                            setHostForm.DisplayMessage("CONNECTED");
                            Get_Orders();
                            Get_Products();
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                    // Indicate tollbooth cannot be created (by using 0 for tollbooth id)
                }
                catch (IOException)
                {
                    // Indicate server unavailable
                }
                catch (SocketException)
                {
                    // Indicate server unavailable
                }
                catch (OutOfMemoryException)
                {
                    // Indicate server unavailable
                }
            }, (HostName, HostPort)); // Thread parameters (in tuple)
        }

        public void Purchase(string product_name)
        {
            ThreadPool.QueueUserWorkItem(threadInfo =>
            {
                (string hostName, int hostPort) = ((string, int))threadInfo; // Deconstruct tuple
                try
                {
                    using (TcpClient tcpClient = new TcpClient()) // Default constructor only allows IPv4
                    {
                        tcpClient.Connect(hostName, hostPort);
                        NetworkStream stream = tcpClient.GetStream();
                        StreamReader reader = new StreamReader(stream);
                        StreamWriter writer = new StreamWriter(stream);
                        writer.WriteLine($"PURCHASE:{product_name}");
                        writer.Flush();
                        string line = reader.ReadLine();
                        if (line != null) // A null value indicates the client may have closed the connection
                        {
                            if (line == "DONE")
                            {
                                Get_Orders();
                                Get_Products();                                
                            }
                            else if (line == "NOT_AVAILABLE")
                            {
                                shopClientForm.DisplayMessage("NOT_AVAILABLE");
                            }
                            else
                            {
                                shopClientForm.DisplayMessage("NOT_VALID");
                            }
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                    // Indicate tollbooth cannot be created (by using 0 for tollbooth id)
                }
                catch (IOException)
                {
                    // Indicate server unavailable
                }
                catch (SocketException)
                {
                    // Indicate server unavailable
                }
                catch (OutOfMemoryException)
                {
                    // Indicate server unavailable
                }
            }, (HostName, HostPort)); // Thread parameters (in tuple)
        }

        // GET_PRODUCTS
        public void Get_Products()
        {
            ThreadPool.QueueUserWorkItem(threadInfo =>
            {
                (string hostName, int hostPort) = ((string, int))threadInfo; // Deconstruct tuple
                try
                {
                    using (TcpClient tcpClient = new TcpClient()) // Default constructor only allows IPv4
                    {
                        tcpClient.Connect(hostName, hostPort);
                        NetworkStream stream = tcpClient.GetStream();
                        StreamReader reader = new StreamReader(stream);
                        StreamWriter writer = new StreamWriter(stream);
                        writer.WriteLine($"GET_PRODUCTS");
                        writer.Flush();
                        string line = reader.ReadLine();
                        if (line != null) // A null value indicates the client may have closed the connection
                        {
                            string[] product = line.Split(':');
                            int j = 0;
                            Products tempProduct = new Products("", 0);

                            if (product[0] == "PRODUCTS")
                            {
                                string[] productInfo = product[1].Split('|');

                                foreach (string i in productInfo)
                                {
                                    string[] nameQuantity = i.Split(',');

                                    if (!string.IsNullOrEmpty(nameQuantity[0]) && nameQuantity[1] != "0")
                                    {
                                        if (!m_currentProducts.TryAdd(j, new Products(nameQuantity[0], Convert.ToInt32(nameQuantity[1]))))
                                        {
                                            tempProduct.ProductName = nameQuantity[0];
                                            tempProduct.Quantity = Convert.ToInt32(nameQuantity[1]);

                                            m_currentProducts.TryUpdate(j, tempProduct, m_currentProducts[j]);
                                        }
                                        j++;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (IOException)
                {
                    // Indicate server unavailable
                }
                catch (SocketException)
                {
                    // Indicate server unavailable
                }
                catch (OutOfMemoryException)
                {
                    // Indicate server unavailable
                }
            }, (HostName, HostPort)); // Thread parameters (in tuple)
        }

        // GET_ORDERS
        public void Get_Orders()
        {
            ThreadPool.QueueUserWorkItem(threadInfo =>
            {
                (string hostName, int hostPort) = ((string, int))threadInfo; // Deconstruct tuple
                try
                {
                    using (TcpClient tcpClient = new TcpClient()) // Default constructor only allows IPv4
                    {
                        tcpClient.Connect(hostName, hostPort);
                        NetworkStream stream = tcpClient.GetStream();
                        StreamReader reader = new StreamReader(stream);
                        StreamWriter writer = new StreamWriter(stream);
                        writer.WriteLine($"GET_ORDERS");
                        writer.Flush();
                        string line = reader.ReadLine();
                        if (line != null) // A null value indicates the client may have closed the connection
                        {
                            string[] order = line.Split(':');
                            int j = 0;
                            Orders tempOrder = new Orders("", 0, "");

                            if (order[0] == "ORDERS")
                            {
                                string[] orderInfo = order[1].Split('|');

                                foreach (string i in orderInfo)
                                {
                                    string[] nameQuantityUsername = i.Split(',');

                                    if(!string.IsNullOrEmpty(nameQuantityUsername[0]) && nameQuantityUsername[1] != "0")
                                    {
                                        if(!m_currentOrders.TryAdd(j, new Orders(nameQuantityUsername[0], Convert.ToInt32(nameQuantityUsername[1]), nameQuantityUsername[2])))
                                        {
                                            if (m_currentOrders[j].ProductName == nameQuantityUsername[0] && m_currentOrders[j].Quantity != Convert.ToInt32(nameQuantityUsername[1]))
                                            {
                                                tempOrder.ProductName = nameQuantityUsername[0];
                                                tempOrder.Quantity = Convert.ToInt32(nameQuantityUsername[1]);
                                                tempOrder.Username = nameQuantityUsername[2];

                                                m_currentOrders.TryUpdate(j, tempOrder, m_currentOrders[j]);
                                            }                                            
                                        }
                                        j++;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (IOException)
                {
                    // Indicate server unavailable
                }
                catch (SocketException)
                {
                    // Indicate server unavailable
                }
                catch (OutOfMemoryException)
                {
                    // Indicate server unavailable
                }
            }, (HostName, HostPort)); // Thread parameters (in tuple)
        }
    }
}
