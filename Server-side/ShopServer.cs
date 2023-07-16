using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_side
{
    public class ShopServer
    {
        private readonly CancellationToken m_cancellationToken;
        public IPAddress ServerIp { get; set; } = IPAddress.Any; // Default to any local ip
        public int ServerPort { get; set; } = 55057; // Default to port 55057
        private readonly ConcurrentDictionary<int, Products> m_currentProducts = new ConcurrentDictionary<int, Products>();
        private readonly ConcurrentDictionary<int, Orders> m_currentOrders = new ConcurrentDictionary<int, Orders>();
        private readonly ConcurrentDictionary<int, Account> m_currentAccount = new ConcurrentDictionary<int, Account>();

        public ShopServer(CancellationToken cancellationToken) => m_cancellationToken = cancellationToken;

        // Handle new client connections
        public void Start()
        {
            string[] items = new string[] { "Apple", "Banana", "Cucumber", "Tomato", "Pear" };
            string[] names = new string[] { "John", "Adam", "Joe" };
            System.Random rand = new Random();

            // Create 5 pre-defined products with random quantities (1-3)
            for (int i = 0; i < 5; i++)
            {
                m_currentProducts.TryAdd(i, new Products(items[i], rand.Next(1, 4)));
            }

            // 3 account createdd
            for (int i = 0; i < 3; i++)
            {
                m_currentAccount.TryAdd(i, new Account(names[i], i + 1));
            }

            try
            {
                TcpListener listener = new TcpListener(ServerIp, ServerPort);
                listener.Start(); // Once the listener is started, the client can connect and send data.  More than one client can connect.
                m_cancellationToken.Register(listener.Stop); // Stop the server port listener if a cancellation is requested
                

                while (!m_cancellationToken.IsCancellationRequested)
                {
                    TcpClient tcpClient = listener.AcceptTcpClient(); // Get the next ready client connection, or wait for a client connection if no new clients are connected
                    ShopClientHandler handler = new ShopClientHandler(tcpClient, m_currentProducts, m_currentOrders, m_currentAccount);
                    ThreadPool.QueueUserWorkItem(handler.Run); // Thread is not long running, so can use thread pool
                }
            }
            catch (SocketException) // Exception takes us out of the loop, server connection handler thread will end
            {
                // SocketException may occur when listener is started or stopped
            }
        }
    }
}
