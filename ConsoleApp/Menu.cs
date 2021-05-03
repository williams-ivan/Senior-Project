using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Menu
    {
        protected int getChoice(int min, int max)
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

        protected virtual void viewAccount() { }
    }
}
