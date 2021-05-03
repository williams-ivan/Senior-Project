//**************************************************
// File: MenuC.cs
//
// Purpose: Displays the menu for customers.
//
// Written By: Ivan Williams
//
// Compiler: Visual Studio 2019
//**************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class MenuC : Menu
    {
        #region Member Variables
        private ObservableCollection<MenuItem> cart;

        private Business mainDispo;
        #endregion

        #region Properties
        public Customer Customer { get; set; }

        public ObservableCollection<Business> Businesses { get; set; }
        #endregion

        #region Member Methods
        //**************************************************
        // Method: Constructor
        //
        // Purpose: Initializing the Customer and Businesses
        //          properties.
        //**************************************************
        public MenuC(Customer c, ObservableCollection<Business> b)
        {
            Customer = c;
            Businesses = b;
            cart = new ObservableCollection<MenuItem>();
            mainMenu();
        }

        //**************************************************
        // Method: mainMenu
        //
        // Purpose: Displaying the main menu.
        //**************************************************
        public void mainMenu()
        {
            Console.Clear();
            int choice;
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Shop");
            Console.WriteLine("2. View Cart");
            Console.WriteLine("3. View Order History");
            Console.WriteLine("4. View Account Information");
            Console.WriteLine("5. Log Out");
            Console.WriteLine("====================================");
            choice = getChoice(1, 5);
            switch (choice)
            {
                case 1:
                    shop();
                    break;
                case 2:
                    if (cart.Count > 0) {
                        viewCart();
                    }
                    else {
                        Console.WriteLine("Your cart is empty.");
                        string wait = Console.ReadLine();
                    }
                    break;
                case 3:
                    if (Customer.Orders != null && Customer.Orders.Count > 0)
                    {
                        viewOrders();
                    }
                    else
                    {
                        Console.WriteLine("You never made an order.");
                        string wait = Console.ReadLine();
                    }
                    break;
                case 4:
                    viewAccount();
                    break;
            }
            if (choice != 5)
            {
                mainMenu();
            }
        }

        //**************************************************
        // Method: shop
        //
        // Purpose: Viewing the inventories of dispensaries
        //          and adding items from them to your cart.
        //**************************************************
        private void shop()
        {
            if (mainDispo == null)
            {
                mainDispo = getDispo();
            }
            if (mainDispo != null)
            {
                if (mainDispo.Items.Count > 0)
                {
                    ObservableCollection<MenuItem[]> inv = new ObservableCollection<MenuItem[]>();
                    int count = 0;
                    MenuItem[] arr = new MenuItem[6];

                    foreach (MenuItem item in mainDispo.Items)
                    {
                        if (count > 5)
                        {
                            inv.Add(arr);
                            count = 0;
                            arr = null;
                        }
                        if (count == 0 && arr == null)
                        {
                            arr = new MenuItem[6];
                        }
                        arr[count] = item;
                        count++;
                    }
                    if (arr != null)
                    {
                        inv.Add(arr);
                    }
                    viewInv(inv);
                    if (cart.Count == 0)
                    {
                        mainDispo = null;
                    }
                }
                else
                {
                    Console.WriteLine("This dispensary doesn't have products yet.");
                    string wait = Console.ReadLine();
                }
            }
        }

        //**************************************************
        // Method: getDispo
        //
        // Purpose: Getting a dispensary to look through.
        //**************************************************
        private Business getDispo()
        {
            Business dispo = null;
            ObservableCollection<Business[]> list = new ObservableCollection<Business[]>();
            int count = 0;
            Business[] arr = new Business[6];

            foreach (Business b in Businesses)
            {
                if (count > 5)
                {
                    list.Add(arr);
                    count = 0;
                    arr = null;
                }
                if (count == 0 && arr == null)
                {
                    arr = new Business[6];
                }
                arr[count] = b;
                count++;
            }
            if (arr != null) {
                list.Add(arr);
            }
            count = 0;

            string choice;
            do {
                Console.Clear();
                string[] choices = new string[9];
                choices[0] = "<";
                choices[1] = ">";
                choices[2] = "0";

                Console.WriteLine("Browse Dispensaries");
                Console.WriteLine("\t\t\t\tPage " + (count + 1));
                Console.WriteLine("====================================");
                int counter = 1;
                foreach (Business b in list[count]) {
                    if (b != null) {
                        Console.WriteLine(counter + ". " + b.Name);
                        choices[counter + 2] = counter.ToString();
                        counter++;
                    }
                }
                Console.WriteLine("0. Exit");
                Console.WriteLine("====================================");
                Console.Write("Enter selection (type '<'/'>' to switch pages): ");
                choice = Console.ReadLine();
                while (Array.Find(choices, c => c == choice) == null)
                {
                    Console.Write("Invalid. Re-enter selection: ");
                    choice = Console.ReadLine();
                }

                if (choice == "<")
                {
                    if (count > 0)
                    {
                        count--;
                    }
                    else
                    {
                        count = list.Count - 1;
                    }
                }
                else if (choice == ">")
                {
                    if (count < list.Count - 1)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                }
                else
                {
                    int n = Convert.ToInt32(choice);
                    if (n < 7 && n > 0)
                    {
                        if (list[count][n - 1].Items.Count == 0)
                        {
                            Console.WriteLine("This dispensary doesn't have products yet.");
                            string wait = Console.ReadLine();
                        }
                        else
                        {
                            dispo = list[count][n - 1];
                        }
                    }
                }
            } while (choice != "0" && dispo == null);

            return dispo;
        }

        //**************************************************
        // Method: viewInv
        //
        // Purpose: Viewing the inventory of a Business.
        //**************************************************
        private void viewInv(ObservableCollection<MenuItem[]> inv)
        {
            string choice;
            int index = 0;
            do
            {
                Console.Clear();
                string[] choices = new string[9];
                choices[0] = "<";
                choices[1] = ">";
                choices[2] = "0";

                Console.WriteLine(mainDispo.Name + "'s Inventory");
                Console.WriteLine("\t\t\t\tPage " + (index + 1));
                Console.WriteLine("====================================");
                int counter = 1;
                foreach (MenuItem item in inv[index])
                {
                    if (item != null)
                    {
                        Console.WriteLine(counter + ". " + item.Name);
                        choices[counter + 2] = counter.ToString();
                        counter++;
                    }
                }
                Console.WriteLine("0. Exit");
                Console.WriteLine("====================================");
                Console.Write("Enter selection (type '<'/'>' to switch pages): ");
                choice = Console.ReadLine();
                while (Array.Find(choices, c => c == choice) == null)
                {
                    Console.Write("Invalid. Re-enter selection: ");
                    choice = Console.ReadLine();
                }

                if (choice == "<")
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = inv.Count - 1;
                    }
                }
                else if (choice == ">")
                {
                    if (index < inv.Count - 1)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
                else
                {
                    int n = Convert.ToInt32(choice);
                    if (n < 7 && n > 0)
                    {
                        viewItem(inv[index][n - 1]);
                    }
                }
            } while (choice != "0");
        }

        //**************************************************
        // Method: viewItem
        //
        // Purpose: Displaying an item's information.
        //**************************************************
        private void viewItem(MenuItem item)
        {
            Console.Clear();
            int choice;
            Console.WriteLine("====================================");
            PropertyInfo[] properties = typeof(MenuItem).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "Information" && property.Name != "PrescriptionLength")
                {
                    Console.WriteLine(property.Name + ": " + ((property.Name == "Price") ? "$" : "") + property.GetValue(item));
                }
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine("**Warning: Once you add items to your cart, you cannot change dispensaries until your cart is empty again.");
            Console.WriteLine("1. Add to Cart");
            Console.WriteLine("2. Cancel");
            Console.WriteLine("====================================");
            choice = getChoice(1, 2);
            if (choice == 1) {
                if (cart.Count < 10)
                {
                    int amount;
                    bool success;
                    do
                    {
                        Console.Write("Enter amount (your cart cannot exceed 10 items): ");
                        success = int.TryParse(Console.ReadLine(), out amount);
                    } while (!success && (amount > 10 || amount < 1));
                    for (int i = 0; i < amount; i++)
                    {
                        cart.Add(item);
                    }
                }
                else
                {
                    Console.WriteLine("Your cart is full.");
                    string wait = Console.ReadLine();
                }
            }
        }

        //**************************************************
        // Method: getTotalPrice
        //
        // Purpose: Getting the total price of an order.
        //**************************************************
        private string getTotalPrice()
        {
            double price = 0;
            foreach (MenuItem item in cart)
            {
                price += Convert.ToDouble(item.Price);
            }
            return price.ToString();
        }

        //**************************************************
        // Method: getAddress
        //
        // Purpose: Getting the address to deliver to.
        //**************************************************
        private Address getAddress(int option)
        {
            Address a = null;
            if (option == 1) {
                ObservableCollection<Address[]> list = new ObservableCollection<Address[]>();
                int count = 0;
                Address[] arr = new Address[6];

                foreach (Address addy in Customer.Addresses)
                {
                    if (count > 5)
                    {
                        list.Add(arr);
                        count = 0;
                        arr = null;
                    }
                    if (count == 0 && arr == null)
                    {
                        arr = new Address[6];
                    }
                    arr[count] = addy;
                    count++;
                }
                if (arr != null)
                {
                    list.Add(arr);
                }
                count = 0;

                string choice;
                do {
                    Console.Clear();
                    string[] choices = new string[8];
                    choices[0] = "<";
                    choices[1] = ">";

                    Console.WriteLine("Select Address to Deliver to");
                    Console.WriteLine("\t\t\t\tPage " + (count + 1));
                    Console.WriteLine("====================================");
                    int counter = 1;
                    foreach (Address addy in list[count])
                    {
                        if (addy != null)
                        {
                            Console.WriteLine(counter + ". " + addy);
                            choices[counter + 2] = counter.ToString();
                            counter++;
                        }
                    }
                    Console.WriteLine("====================================");
                    Console.Write("Enter selection (type '<'/'>' to switch pages): ");
                    choice = Console.ReadLine();
                    while (Array.Find(choices, c => c == choice) == null)
                    {
                        Console.Write("Invalid. Re-enter selection: ");
                        choice = Console.ReadLine();
                    }

                    if (choice == "<")
                    {
                        if (count > 0)
                        {
                            count--;
                        }
                        else
                        {
                            count = list.Count - 1;
                        }
                    }
                    else if (choice == ">")
                    {
                        if (count < list.Count - 1)
                        {
                            count++;
                        }
                        else
                        {
                            count = 0;
                        }
                    }
                    else
                    {
                        int n = Convert.ToInt32(choice);
                        a = list[count][n - 1];
                    }
                } while (a == null);
            }
            else {
                Console.Clear();
                Console.WriteLine("Enter Address to Deliver to");
                a = new Address();
                PropertyInfo[] ap = typeof(Address).GetProperties();
                foreach (PropertyInfo p in ap)
                {
                    Console.Write(p.Name + ": ");
                    p.SetValue(a, Console.ReadLine());
                }
                Customer.Addresses.Add(a);
            }
            return a;
        }

        //**************************************************
        // Method: viewCart
        //
        // Purpose: Viewing and editing the cart.
        //**************************************************
        private void viewCart()
        {
            string choice;
            string[] choices = new string[cart.Count + 3];
            choices[0] = "b";
            choices[1] = "c";
            choices[2] = "o";
            for (int i = 0; i < cart.Count; i++) {
                choices[i + 3] = (i + 1).ToString();
            }

            do {
                Console.Clear();
                Console.WriteLine("Cart\t\t\t\t$" + getTotalPrice());
                Console.WriteLine("====================================");
                int count = 1;
                foreach (MenuItem item in cart)
                {
                    if (count > 1)
                    {
                        Console.WriteLine("------------------------------------");
                    }
                    Console.WriteLine(count + ". " + item.Name);
                    count++;
                }
                Console.WriteLine("====================================");
                Console.WriteLine("Options: (B)ack, (C)lear, Check(O)ut");
                Console.Write("Enter item number or option above: ");
                choice = Console.ReadLine().ToLower();
                while (Array.Find(choices, c => c == choice) == null)
                {
                    Console.Write("Invalid. Re-enter selection: ");
                    choice = Console.ReadLine();
                }
                if (choice == "o" || choice == "c")
                {
                    if (choice == "o")
                    {
                        Address a;
                        if (Customer.Addresses.Count == 0)
                        {
                            a = getAddress(2);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("1. Select an address");
                            Console.WriteLine("2. Enter a new address");
                            int selection = getChoice(1, 2);
                            a = getAddress(selection);
                        }
                        Order o = new Order();
                        o.Date = DateTime.Today.ToString("d");
                        o.TotalPrice = getTotalPrice();
                        o.DasherShare = "0.15";
                        o.Status = "Pending";
                        o.Business = mainDispo;
                        o.Address = a;
                        foreach (MenuItem item in cart)
                        {
                            o.Items.Add(item);
                        }
                        Customer.Orders.Add(o);
                    }
                    cart.Clear();
                }
                else if (choice != "b")
                {
                    int answer, n = Convert.ToInt32(choice) - 1;
                    Console.WriteLine("1. Remove from Cart");
                    Console.WriteLine("2. Cancel");
                    answer = getChoice(1, 2);
                    if (answer == 1)
                    {
                        cart.RemoveAt(n);
                    }
                }
            } while (choice != "b" && cart.Count > 0);
            if (cart.Count == 0) {
                mainDispo = null;
            }
        }

        //**************************************************
        // Method: viewOrders
        //
        // Purpose: Viewing order history.
        //**************************************************
        private void viewOrders()
        {
            int n = 1;
            string choice;
            do
            {
                Console.Clear();
                string[] choices = new string[9];
                choices[0] = "<";
                choices[1] = ">";
                choices[2] = "0";
                Console.WriteLine("Your Orders");
                Console.WriteLine("\t\t\t\tPage " + n);
                Console.WriteLine("====================================");
                for (int i = n; i < n + 5; i++)
                {
                    if (Customer.Orders.Count >= i)
                    {
                        if (i > n)
                        {
                            Console.WriteLine("------------------------------------");
                        }
                        Console.WriteLine("{0}. {1} {2} {3}", i, Customer.Orders[i - 1].TotalPrice, Customer.Orders[i - 1].Date, Customer.Orders[i - 1].Status);
                        choices[(i - n) + 3] = i.ToString();
                    }
                }
                Console.WriteLine("------------------------------------");
                Console.WriteLine("0. Exit");
                Console.WriteLine("====================================");
                choice = Console.ReadLine();
                while (Array.Find(choices, c => c == choice) == null)
                {
                    Console.Write("Invalid. Re-enter selection: ");
                    choice = Console.ReadLine();
                }

                if (choice == "<")
                {
                    if (n > 1)
                    {
                        n -= 6;
                    }
                }
                else if (choice == ">")
                {
                    if (n < Customer.Orders.Count && Customer.Orders.Count >= n + 6)
                    {
                        n += 6;
                    }
                }
                else if (choice != "0")
                {
                    Console.Clear();
                    int index = Convert.ToInt32(choice) - 1;
                    Console.WriteLine("====================================");
                    PropertyInfo[] properties = typeof(Order).GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.Name == "Items")
                        {
                            foreach (MenuItem item in Customer.Orders[index].Items)
                            {
                                Console.WriteLine("------------------------------------");
                                PropertyInfo[] ip = typeof(MenuItem).GetProperties();
                                foreach (PropertyInfo p in ip)
                                {
                                    Console.WriteLine(p.Name + ": " + ((p.Name == "Price") ? "$" : "") + p.GetValue(item));
                                }
                            }
                        }
                        else if (property.Name != "DasherShare")
                        {
                            Console.WriteLine(property.Name + ": " + ((property.Name == "TotalPrice") ? "$" : "") + property.GetValue(Customer.Orders[index]));
                        }
                    }
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("1. Print");
                    Console.WriteLine((Customer.Orders[index].Status == "Completed") ? "2. Reorder" : "2. Order Received");
                    Console.WriteLine("3. Cancel");
                    Console.WriteLine("====================================");
                    int answer = getChoice(1, 3);
                    if (answer == 1)
                    {

                        string filename, str = "";
                        foreach (PropertyInfo property in properties)
                        {
                            if (property.Name != "Items" && property.Name != "DasherShare")
                            {
                                str += property.Name + ": " + property.GetValue(Customer.Orders[index]) + "\n";
                            }
                        }
                        Console.Write("Enter file name: ");
                        filename = Console.ReadLine();
                        File.WriteAllText(filename, str);
                    }
                    else if (answer == 2)
                    {
                        if (Customer.Orders[index].Status == "Completed")
                        {
                            Order o = new Order();
                            o.Date = DateTime.Today.ToString("d");
                            o.TotalPrice = Customer.Orders[index].TotalPrice;
                            o.DasherShare = Customer.Orders[index].DasherShare;
                            o.Status = "Pending";
                            o.Business = Customer.Orders[index].Business;
                            o.Items = Customer.Orders[index].Items;
                            Customer.Orders.Add(o);
                        }
                        else if (Customer.Orders[index].Status == "Delivered")
                        {
                            Customer.Orders[index].Status = "Completed";
                        }
                        else
                        {
                            Console.WriteLine("This order hasn't been delivered yet.");
                        }
                    }
                }
            } while (choice != "0");
        }

        //**************************************************
        // Method: viewAccount
        //
        // Purpose: Displaying the Customer's account info.
        //**************************************************
        protected override void viewAccount()
        {
            Console.Clear();
            Console.WriteLine("Your Account\n");
            PropertyInfo[] properties = typeof(Customer).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "Addresses" && property.Name != "Orders")
                {
                    Console.WriteLine(property.Name + ": " + property.GetValue(Customer));
                }
            }
            string wait = Console.ReadLine();
        }
        #endregion
    }
}
