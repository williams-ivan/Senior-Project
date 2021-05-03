//**************************************************
// File: MenuD.cs
//
// Purpose: Displays the menu for dashers.
//
// Written By: Ivan Williams
//
// Compiler: Visual Studio 2019
//**************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class MenuD : Menu
    {
        #region Properties
        public Person Dasher { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }
        #endregion

        #region Member Methods
        //**************************************************
        // Method: Constructor
        //
        // Purpose: Initializing the Dasher and Customers
        //          properties.
        //**************************************************
        public MenuD(Person d, ObservableCollection<Customer> c)
        {
            Dasher = d;
            Customers = c;
            mainMenu();
        }

        //**************************************************
        // Method: mainMenu
        //
        // Purpose: Displaying the main menu.
        //**************************************************
        public override void mainMenu()
        {
            Console.Clear();
            int choice;
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("====================================");
            Console.WriteLine("1. View Orders Available");
            Console.WriteLine("2. View Orders Taken");
            Console.WriteLine("3. View Orders Delivered");
            Console.WriteLine("4. View Account Information");
            Console.WriteLine("5. Log Out");
            Console.WriteLine("====================================");
            choice = getChoice(1, 5);
            switch (choice)
            {
                case 1:
                    viewOrders("free");
                    break;
                case 2:
                    viewOrders("taken");
                    break;
                case 3:
                    viewOrders("done");
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
        // Method: viewOrders
        //
        // Purpose: Viewing orders to claim and deliver.
        //**************************************************
        private void viewOrders(string type)
        {
            int n = 0;
            string choice = "0";
            do
            {
                ObservableCollection<Order> temp = new ObservableCollection<Order>();
                foreach (Order o in Customers[n].Orders)
                {
                    if ((type == "done") ? ((o.Status == "Delivered" || o.Status == "Completed") && o.Dasher == Dasher) : (type == "taken") ? (o.Status == "In Progress" && o.Dasher == Dasher) : (o.Status == "Pending"))
                    {
                        temp.Add(o);
                    }
                }
                if (temp.Count > 0)
                {
                    Console.Clear();
                    Console.WriteLine((type == "done") ? "Orders Delivered" : (type == "taken") ? "Orders Accepted" : "Available Orders");
                    Console.WriteLine("\t\t\t\tPage " + n);
                    Console.WriteLine("====================================");
                    int count = 1;
                    foreach (Order o in temp)
                    {
                        if (count > 1)
                        {
                            Console.WriteLine("------------------------------------");
                        }
                        Console.WriteLine("{0}. {1} {2} {3}", count, o.TotalPrice, o.Date, o.Status);
                        count++;
                    }
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("0. Exit");
                    Console.WriteLine("====================================");
                    string[] choices = new string[count + 2];
                    choices[0] = "<";
                    choices[1] = ">";
                    choices[2] = "0";
                    for (int i = 1; i < count; i++)
                    {
                        choices[i + 2] = i.ToString();
                    }
                    Console.Write("Enter selection (type '<'/'>' to switch pages): ");
                    choice = Console.ReadLine();
                    while (Array.Find(choices, c => c == choice) == null)
                    {
                        Console.Write("Invalid. Re-enter selection: ");
                        choice = Console.ReadLine();
                    }

                    if (choice == "<")
                    {
                        if (n > 0)
                        {
                            n--;
                        }
                        else
                        {
                            n = Customers.Count - 1;
                        }
                    }
                    else if (choice == ">")
                    {
                        if (n < Customers.Count - 1)
                        {
                            n++;
                        }
                        else
                        {
                            n = 0;
                        }
                    }
                    else if (choice != "0")
                    {
                        Console.Clear();
                        int index = Convert.ToInt32(choice) - 1;
                        Console.WriteLine("====================================");
                        Console.WriteLine("Customer: " + Customers[n].Email);
                        PropertyInfo[] properties = typeof(Order).GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            if (property.Name == "DasherShare")
                            {
                                double share = Convert.ToDouble(property.GetValue(temp[index]));
                                share *= Convert.ToDouble(temp[index].TotalPrice);
                                Console.WriteLine("DasherShare: $" + share);

                            }
                            else if (property.Name != "Items")
                            {
                                Console.WriteLine(property.Name + ": " + ((property.Name == "TotalPrice") ? "$" : "") + property.GetValue(temp[index]));
                            }
                        }
                        Console.WriteLine("====================================");
                        if (type != "done")
                        {
                            string answer;
                            do
                            {
                                Console.WriteLine(((type == "taken") ? "Delivered" : "Claim") + "? (Y/N)");
                                answer = Console.ReadLine().ToLower();
                                Console.Write((answer != "y" && answer != "n") ? "Invalid. " : "");
                            } while (answer != "y" && answer != "n");
                            if (answer == "y")
                            {
                                if (type == "taken")
                                {
                                    temp[index].Status = "Delivered";
                                    Console.WriteLine("Order Delivered");
                                }
                                else
                                {
                                    temp[index].Dasher = Dasher;
                                    temp[index].Status = "In Progress";
                                    Console.WriteLine("Delivery In Progress");
                                }
                                string wait = Console.ReadLine();
                            }
                        }
                        else
                        {
                            string wait = Console.ReadLine();
                        }
                    }
                }
                else {
                    Console.WriteLine("There are currently no orders.");
                    choice = "0";
                    string wait = Console.ReadLine();
                }
            } while (choice != "0");
        }

        //**************************************************
        // Method: viewAccount
        //
        // Purpose: Displaying the Dasher's account info.
        //**************************************************
        protected override void viewAccount()
        {
            Console.Clear();
            Console.WriteLine("Your Account\n");
            PropertyInfo[] properties = typeof(Person).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "Addresses")
                {
                    Console.WriteLine(property.Name + ": " + property.GetValue(Dasher));
                }
            }
            string wait = Console.ReadLine();
        }
        #endregion
    }
}
