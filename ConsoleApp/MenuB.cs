using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ConsoleApp
{
    class MenuB
    {
        public Business Business { get; set; }
        public ObservableCollection<Business> Businesses { get; set; }
        public MenuB(Business b, ObservableCollection<Business> bl)
        {
            Business = b;
            Businesses = bl;
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
            Console.Write("Enter selection: ");
            choice = int.Parse(Console.ReadLine());
            while (choice > 4 || choice < 1) {
                Console.Write("Invalid. Re-enter selection: ");
                choice = int.Parse(Console.ReadLine());
            }
            switch (choice) {
                case 1:
                    addItem();
                    break;
                case 2:
                    //removeItem();
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            if (choice == 2 || choice == 3) {
                Console.WriteLine("In Construction");
                string wait = Console.ReadLine();
            }
            if (choice != 4) {
                mainMenu();
            }
        }
        private int getChoice(int min, int max)
        {
            string input;

            int choice;
            Console.Write("Enter selection: ");
            choice = int.Parse(Console.ReadLine());
            while (choice > max || choice < min)
            {
                Console.Write("Invalid. Re-enter selection: ");
                choice = int.Parse(Console.ReadLine());
            }
            return choice;
        }
        private MenuItem getItem(string name) {
            foreach (MenuItem item in Business.Items)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
        private void addItem()
        {
            Console.Clear();
            MenuItem item = new MenuItem();
            PropertyInfo[] properties = typeof(MenuItem).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Console.Write(property.Name + ": ");
                property.SetValue(item, Console.ReadLine());
            }
            if (getItem(item.Name) == null)
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
        private void removeItem()
        {
            Console.Clear();
            MenuItem item;
            string name;
            Console.Write("Enter the name of the item: ");
            name = Console.ReadLine();
            item = getItem(name);
            if (item != null)
            {
                Business.Items.Remove(item);
                Console.WriteLine("The item (" + name + ") was successfully removed.");
            }
            else
            {
                Console.WriteLine("The item (" + name + ") does not exist.");
            }
            string wait = Console.ReadLine();
        }
    }
}
