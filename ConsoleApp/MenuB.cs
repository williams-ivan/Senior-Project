using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ConsoleApp
{
    class MenuB
    {
        public Business Business { get; set; }
        public ObservableCollection<Business> Others { get; set; }
        public MenuB(Business b, ObservableCollection<Business> o)
        {
            Business = b;
            Others = o;
            mainMenu();
        }
        public void mainMenu()
        {
            Console.Clear();
            int choice;
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. View Inventory");
            Console.WriteLine("3. View Dispensaries");
            Console.WriteLine("4. Log Out");
            Console.WriteLine("====================================");
            choice = getChoice(1, 4);
            switch (choice) {
                case 1:
                    addItem();
                    break;
                case 2:
                    viewInv(Business);
                    break;
                case 3:
                    viewDispos();
                    break;
                case 4:
                    break;
            }
            if (choice != 4) {
                mainMenu();
            }
        }
        private int getChoice(int min, int max)
        {
            int choice;
            Console.Write("Enter selection: ");
            bool success = int.TryParse(Console.ReadLine(), out choice);
            while (success && (choice > max || choice < min))
            {
                Console.Write("Invalid. Re-enter selection: ");
                success = int.TryParse(Console.ReadLine(), out choice);
            }
            return choice;
        }
        private bool check(string name) {
            foreach (MenuItem item in Business.Items)
            {
                if (item.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        private void addItem()
        {
            Console.Clear();
            Console.WriteLine("Please enter the following information as it appears");
            MenuItem item = new MenuItem();
            PropertyInfo[] properties = typeof(MenuItem).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string input;
                Console.Write(property.Name + ": ");
                input = Console.ReadLine();
                property.SetValue(item, input);
                if (property.Name == "Price") {
                    double tryDouble;
                    bool success = double.TryParse(input, out tryDouble);
                    while (!success)
                    {
                        Console.Write(property.Name + ": ");
                        input = Console.ReadLine();
                        property.SetValue(item, input);
                        success = double.TryParse(input, out tryDouble);
                    }
                }
            }
            if (!check(item.Name))
            {
                Business.Items.Add(item);
                Console.WriteLine("The item (" + item.Name + ") was successfully added.");
            }
            else
            {
                Console.WriteLine("This item was already added.");
            }
            string wait = Console.ReadLine();
        }
        private void viewDispos()
        {
            ObservableCollection<Business[]> list = new ObservableCollection<Business[]>();
            int count = 0;
            Business[] arr = new Business[6];

            foreach (Business b in Others)
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
            if (arr != null)
            {
                list.Add(arr);
            }
            count = 0;

            string choice;
            do
            {
                Console.Clear();
                string[] choices = new string[9];
                choices[0] = "<";
                choices[1] = ">";
                choices[2] = "0";

                Console.WriteLine("Browse Dispensaries");
                Console.WriteLine("\t\t\t\tPage " + (count + 1));
                Console.WriteLine("====================================");
                int counter = 1;
                foreach (Business b in list[count])
                {
                    if (b != null)
                    {
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
                            viewInv(list[count][n - 1]);
                        }
                    }
                }
            } while (choice != "0");
        }
        private void viewInv(Business dispo)
        {
            ObservableCollection<MenuItem[]> inv = new ObservableCollection<MenuItem[]>();
            int count = 0;
            MenuItem[] arr = new MenuItem[6];

            foreach (MenuItem item in dispo.Items)
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
            count = 0;

            string choice;
            do
            {
                Console.Clear();
                string[] choices = new string[9];
                choices[0] = "<";
                choices[1] = ">";
                choices[2] = "0";

                Console.WriteLine(dispo.Name + "'s Inventory");
                Console.WriteLine("\t\t\t\tPage " + (count + 1));
                Console.WriteLine("====================================");
                int counter = 1;
                foreach (MenuItem item in inv[count])
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
                    if (count > 0)
                    {
                        count--;
                    }
                    else
                    {
                        count = inv.Count - 1;
                    }
                }
                else if (choice == ">")
                {
                    if (count < inv.Count - 1)
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
                        viewItem(inv[count][n - 1], dispo == Business);
                    }
                }
            } while (choice != "0");
        }
        private void viewItem(MenuItem item, bool main)
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
            if (main) {
                Console.WriteLine("------------------------------------");
                Console.WriteLine("1. Remove Item");
                Console.WriteLine("2. Cancel");
            }
            Console.WriteLine("====================================");
            if (main)
            {
                choice = getChoice(1, 2);
                if (choice == 1)
                {
                    Business.Items.Remove(item);
                }
            }
            else
            {
                string wait = Console.ReadLine();
            }
        }
    }
}
