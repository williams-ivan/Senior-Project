//**************************************************
// File: Menu.cs
//
// Purpose: Contains methods used by all menus.
//
// Written By: Ivan Williams
//
// Compiler: Visual Studio 2019
//**************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Menu
    {
        #region Member Methods
        //**************************************************
        // Method: getChoice
        //
        // Purpose: Getting solely numerical user input.
        //**************************************************
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

        //**************************************************
        // Method: mainMenu
        //
        // Purpose: For overriding.
        //**************************************************
        public virtual void mainMenu() { }

        //**************************************************
        // Method: viewAccount
        //
        // Purpose: For overriding.
        //**************************************************
        protected virtual void viewAccount() { }
        #endregion
    }
}
