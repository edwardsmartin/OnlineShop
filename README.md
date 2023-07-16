# Online Shop
# Description
An online shopping application that uses both client and server-side using Sockets from the .NET Framework, Multithreading, and Asynchronous programming concepts.

# Server-side    
- A console application that prints each response to the output window before sending it back to the client-side(s).
- When it first runs, five pre-defined products with random quantities (1-3) should be initialized. Products are the same every time the server starts, and the quantities for each product should be randomized.
- Three accounts are created, each account has an account number and user’s name.
- When the server is active, it stores ordering information to __ConcurrentDictionary__. The information is disposed of once the server is shut down.
- The server should send back an appropriate response depending on the client’s command. (Protocols are listed below)
- The handler code is separated from the user interface in a separate class.
- The server must be able to talk with multiple clients at the same time.

# Client-side
- The client side is in a Windows Forms application, with an appropriate user interface. The user can select one product, then make the purchase. (quantity as one)
- When it first opens, a login form appears with two input fields: hostname/IP and the account number. (localhost as the default value for the hostname)
- If the server cannot be found, the application displays an error message; if the server is active but the login failed, a different error message is displayed.
- Once the user successfully connected to the server, the application should get all of the products information (names and quantities), then show all information on the GUI.
- The user can gracefully disconnect from the server. Upon disconnecting, the application closes. The application gracefully disconnects when the form is closed.
- When the user makes the purchase, if the product is not available, the application displays an appropriate message stating that the product is no longer available.
- The application can show current purchase orders.
- The server handler code is separated from the user interface  in a separate class.
- Code that writes to or reads from the server does not block the GUI thread using multithreading or asynchronous programming to prevent blocking the GUI thread.

# Protocol Standards for this Project:
Client Commands  | Server Response
------------- | -------------
DISCONNECT  | No response. <br />_The server removes the client from the list of active clients. Both sides end the connection._
CONNECT:account_no  | CONNECTED:user_name <br />_The client has successfully connected with the specified account number. The server returns the connected client's name._<br />CONNECT_ERROR<br />_The client's connection attempt is unsuccessful. The account_no is not valid._
GET_PRODUCTS  | PRODUCTS:product_name1,quantity1\|product_name2,quantity2\|...<br />_The server sends all product information (e.g. PRODUCTS:APPLE,2\|ORANGE,1)_
GET_ORDERS  | ORDERS:product_name1,quantity1,user_name\|product_name2,quantity2,user_name\|...<br />_The server sends the purchase orders of all clients. (e.g. ORDERS:APPLE,1,John\|ORANGE,1,Doe)._
PURCHASE:product_name  | DONE <br />_The order was successfully placed._ <br /> NOT_AVAILABLE <br />_The product is not available (i.e., is already purchased by another client) and cannot be purchased._ <br />NOT_VALID <br /> _The specified product is not valid._
