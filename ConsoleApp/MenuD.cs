using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class MenuD
    {
        public Person Dasher { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public MenuD(Person d, ObservableCollection<Customer> c)
        {
            Dasher = d;
            Customers = c;
            mainMenu();
        }
        public void mainMenu()
        {
            Console.Clear();
            int choice;
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("====================================");
            Console.WriteLine("1. View Orders Available");
            Console.WriteLine("2. View Orders Taken");
            Console.WriteLine("3. Log Out");
            Console.WriteLine("====================================");
            Console.Write("Enter selection: ");
            choice = Convert.ToInt32(Console.ReadLine());
            while (choice > 3 || choice < 1)
            {
                Console.Write("Invalid. Re-enter selection: ");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            switch (choice)
            {
                case 1:
                    viewOrders("free");
                    break;
                case 2:
                    viewOrders("taken");
                    break;
                case 3:
                    break;
            }
            if (choice != 3)
            {
                mainMenu();
            }
        }
        private void viewOrders(string type)
        {
            int n = 0;
            string choice = "0";
            do
            {
                ObservableCollection<Order> temp = new ObservableCollection<Order>();
                foreach (Order o in Customers[n].Orders)
                {
                    if ((type == "taken") ? (o.Status == "In Progress" && o.Dasher == Dasher.Email) : (o.Status == "Pending"))
                    {
                        temp.Add(o);
                    }
                }
                if (temp.Count > 0)
                {
                    Console.Clear();
                    Console.WriteLine((type == "taken") ? "Orders Accepted" : "Available Orders");
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
                    Console.Write("Enter selection (arrow keys to switch pages): ");
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
                    }
                    else if (choice == ">")
                    {
                        if (n < Customers.Count - 1)
                        {
                            n++;
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
                            if (property.Name != "Items")
                            {
                                Console.WriteLine(property.Name + ": " + ((property.Name == "TotalPrice") ? "$" : "") + property.GetValue(temp[index]));
                            }
                        }
                        Console.WriteLine("====================================");
                        string answer;
                        do
                        {
                            Console.WriteLine(((type == "taken") ? "Delivered" : "Claim") + "? (Y/N)");
                            answer = Console.ReadLine();
                            Console.Write((answer.ToLower() != "y" && answer.ToLower() != "n") ? "Invalid. " : "");
                        } while (answer.ToLower() != "y" && answer.ToLower() != "n");
                        if (answer.ToLower() == "y")
                        {
                            if (type == "taken")
                            {
                                temp[index].Status = "Delivered";
                                Console.WriteLine("Order Delivered");
                            }
                            else
                            {
                                temp[index].Dasher = Dasher.Email;
                                temp[index].Status = "In Progress";
                                Console.WriteLine("Delivery In Progress");
                            }
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
    }
}
