using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class MenuB
    {
        public Business Business { get; set; }
        public MenuB(Business b)
        {
            Business = b;
            mainMenu();
        }
        public void mainMenu()
        {
            Console.Clear();
            int choice;
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Remove Item");
            Console.WriteLine("3. Log Out");
            Console.WriteLine("====================================");
            Console.Write("Enter selection: ");
            choice = Convert.ToInt32(Console.ReadLine());
            while (choice > 3 || choice < 1) {
                Console.Write("Invalid. Re-enter selection: ");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            switch (choice) {
                case 1:
                    addItem();
                    break;
                case 2:
                    removeItem();
                    break;
                case 3:
                    break;
            }
            if (choice != 3) {
                mainMenu();
            }
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
