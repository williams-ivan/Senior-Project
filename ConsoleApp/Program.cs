﻿//**************************************************
// File: Program.cs
//
// Purpose: Registering and logging an account.
//
// Written By: Ivan Williams
//
// Compiler: Visual Studio 2019
//**************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static string filename = "data.json";

        static string logo = "    :::       ::: :::::::::: :::::::::: :::::::::  :::::::::      :::      ::::::::  :::    ::: \n   :+:       :+: :+:        :+:        :+:    :+: :+:    :+:   :+: :+:   :+:    :+: :+:    :+:  \n  +:+       +:+ +:+        +:+        +:+    +:+ +:+    +:+  +:+   +:+  +:+        +:+    +:+   \n +#+  +:+  +#+ +#++:++#   +#++:++#   +#+    +:+ +#+    +:+ +#++:++#++: +#++:++#++ +#++:++#++    \n+#+ +#+#+ +#+ +#+        +#+        +#+    +#+ +#+    +#+ +#+     +#+        +#+ +#+    +#+     \n#+#+# #+#+#  #+#        #+#        #+#    #+# #+#    #+# #+#     #+# #+#    #+# #+#    #+#      \n###   ###   ########## ########## #########  #########  ###     ###  ########  ###    ###       \n";

        static DataList dataStore;

        //**************************************************
        // Method: save
        //
        // Purpose: Saving everything to the database.
        //**************************************************
        static void save() {
            FileStream writer = new FileStream(filename, FileMode.Create, FileAccess.Write);

            DataContractJsonSerializer ser;
            ser = new DataContractJsonSerializer(typeof(DataList));

            ser.WriteObject(writer, dataStore);
            writer.Dispose();
        }

        //**************************************************
        // Method: cCheck
        //
        // Purpose: Checking Customers list for account.
        //**************************************************
        static bool cCheck(string prop, string val) {
            foreach (Customer c in dataStore.Customers)
            {
                if (((prop == "email") ? c.Email : c.Password) == val)
                {
                    return true;
                }
            }
            return false;
        }

        //**************************************************
        // Method: bCheck
        //
        // Purpose: Checking Businesses list for account.
        //**************************************************
        static bool bCheck(string prop, string val)
        {
            foreach (Business b in dataStore.Businesses)
            {
                if (((prop == "email") ? b.Email : b.Password) == val)
                {
                    return true;
                }
            }
            return false;
        }

        //**************************************************
        // Method: dCheck
        //
        // Purpose: Checking Dashers list for account.
        //**************************************************
        static bool dCheck(string prop, string val)
        {
            foreach (Person d in dataStore.Dashers)
            {
                if (((prop == "email") ? d.Email : d.Password) == val)
                {
                    return true;
                }
            }
            return false;
        }

        //**************************************************
        // Method: checkPass
        //
        // Purpose: Checking account's password.
        //**************************************************
        static bool checkPass(string accountType, string pass)
        {
            bool val = false;
            switch (accountType)
            {
                case "c":
                    val = cCheck("pass", pass);
                    break;
                case "b":
                    val = bCheck("pass", pass);
                    break;
                case "d":
                    val = dCheck("pass", pass);
                    break;
            }
            return val;
        }

        //**************************************************
        // Method: check
        //
        // Purpose: Checking if account exists.
        //**************************************************
        static bool check(string accountType, string email, string[] args) {
            bool val = false;
            string[] otherTypes = new string[2];
            switch (accountType) {
                case "c":
                    val = cCheck("email", email);
                    otherTypes[0] = "b";
                    otherTypes[1] = "d";
                    break;
                case "b":
                    val = bCheck("email", email);
                    otherTypes[0] = "c";
                    otherTypes[1] = "d";
                    break;
                case "d":
                    val = dCheck("email", email);
                    otherTypes[0] = "c";
                    otherTypes[1] = "b";
                    break;
            }
            if (!val) {
                foreach (string type in otherTypes)
                {
                    val = (type == "c") ? cCheck("email", email) : (type == "b") ? bCheck("email", email) : dCheck("email", email);
                    if (val)
                    {
                        break;
                    }
                }
            }
            if (args[0] == "true") {
                val = checkPass(accountType, args[1]);
            }
            return val;
        }

        //**************************************************
        // Method: cRegister
        //
        // Purpose: Registering a Customer account.
        //**************************************************
        static void cRegister() {
            Customer c = new Customer();
            string[] args = { "false" };
            PropertyInfo[] properties = typeof(Customer).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "Addresses" && property.Name != "Orders")
                {
                    Console.Write(property.Name + ": " );
                    property.SetValue(c, Console.ReadLine());
                }
            }
            if (!check("c", c.Email, args))
            {
                dataStore.Customers.Add(c);
                save();
                MenuC menu = new MenuC(c, dataStore.Businesses);
            }
            else
            {
                Console.Write("An account was already made with this email.");
                string wait = Console.ReadLine();
            }
        }

        //**************************************************
        // Method: bRegister
        //
        // Purpose: Registering a Business account.
        //**************************************************
        static void bRegister()
        {
            Business b = new Business();
            string[] args = { "false" };
            PropertyInfo[] properties = typeof(Business).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Address")
                {
                    b.Address = new Address();
                    PropertyInfo[] ap = typeof(Address).GetProperties();
                    foreach (PropertyInfo p in ap)
                    {
                        Console.Write(p.Name + ": ");
                        p.SetValue(b.Address, Console.ReadLine());
                    }
                }
                else if (property.Name != "Items")
                {
                    Console.Write(property.Name + ": ");
                    property.SetValue(b, Console.ReadLine());
                }
            }
            if (!check("b", b.Email, args))
            {
                dataStore.Businesses.Add(b);
                save();
                MenuB menu = new MenuB(b, dataStore.Businesses);
            }
            else
            {
                Console.Write("An account was already made with this email.");
                string wait = Console.ReadLine();
            }
        }

        //**************************************************
        // Method: dRegister
        //
        // Purpose: Registering a Dasher account.
        //**************************************************
        static void dRegister()
        {
            Person d = new Person();
            string[] args = { "false" };
            PropertyInfo[] properties = typeof(Person).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "Addresses")
                {
                    Console.Write(property.Name + ": ");
                    property.SetValue(d, Console.ReadLine());
                }
            }
            if (!check("d", d.Email, args))
            {
                dataStore.Dashers.Add(d);
                save();
                MenuD menu = new MenuD(d, dataStore.Customers);
            }
            else
            {
                Console.Write("An account was already made with this email.");
                string wait = Console.ReadLine();
            }
        }

        //**************************************************
        // Method: register
        //
        // Purpose: Selecting type of registry.
        //**************************************************
        static void register() {
            Console.Clear();
            string choice;
            do
            {
                Console.WriteLine("(C)ustomer, (B)usiness, or (D)asher?");
                choice = Console.ReadLine().ToLower();
                Console.Write((choice != "b" && choice != "c" && choice != "d") ? "Invalid. " : "");
            } while (choice != "b" && choice != "c" && choice != "d");

            Console.Clear();
            Console.WriteLine("Please enter the following information as it appears");

            switch (choice)
            {
                case "b":
                    bRegister();
                    break;
                case "c":
                    cRegister();
                    break;
                case "d":
                    dRegister();
                    break;
            }
            save();
        }

        //**************************************************
        // Method: getAccount
        //
        // Purpose: Getting an account.
        //**************************************************
        static Account getAccount(string email, string pass) {
            foreach (var property in dataStore.GetType().GetProperties())
            {
                if (property.Name != "Accounts")
                {
                    foreach (Account a in (IEnumerable)property.GetValue(dataStore))
                    {
                        if (a.Email == email && a.Password == pass)
                        {
                            return a;
                        }
                    }
                }
            }
            return null;
        }

        //**************************************************
        // Method: login
        //
        // Purpose: Logging in.
        //**************************************************
        static void login() {
            Console.Clear();
            Account a;
            string email, password = "";
            string[] args = { "true", password };
            Console.Write("Email: ");
            email = Console.ReadLine();
            Console.Write("Password: ");
            password = Console.ReadLine();
            a = getAccount(email, password);
            if (a != null)
            {
                Console.WriteLine("Welcome back!");
                string wait = Console.ReadLine();
                if (cCheck("email", a.Email))
                {
                    MenuC menu = new MenuC((Customer)a, dataStore.Businesses);
                }
                else if (bCheck("email", a.Email))
                {
                    MenuB menu = new MenuB((Business)a, dataStore.Businesses);
                }
                else if (dCheck("email", a.Email))
                {
                    MenuD menu = new MenuD((Person)a, dataStore.Customers);
                }
                save();
            }
            else
            {
                Console.WriteLine("Invalid user.");
                string wait = Console.ReadLine();
            }
        }

        //**************************************************
        // Method: Main
        //
        // Purpose: Running the app and prompting for
        //          registry or login.
        //**************************************************
        static void Main(string[] args)
        {
            FileStream reader = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
            DataContractJsonSerializer inputSerializer;
            inputSerializer = new DataContractJsonSerializer(typeof(DataList));
            if (reader.Length == 0) {
                dataStore = new DataList();
                dataStore.Customers = new ObservableCollection<Customer>();
                dataStore.Dashers = new ObservableCollection<Person>();
                dataStore.Businesses = new ObservableCollection<Business>();
                dataStore.Accounts = new ObservableCollection<Account>();
            }
            else {
                dataStore = (DataList)inputSerializer.ReadObject(reader);
            }
            reader.Dispose();

            string choice, answer;

            do
            {
                Console.Clear();
                do
                {
                    Console.WriteLine(logo);
                    Console.WriteLine("\n\n\n(L)ogin or (R)egister?");
                    choice = Console.ReadLine().ToLower();
                    Console.Write((choice != "l" && choice != "r") ? "Invalid. " : "");
                } while (choice != "l" && choice != "r");
                switch (choice)
                {
                    case "r":
                        register();
                        break;
                    case "l":
                        login();
                        break;
                }
                save();
                Console.WriteLine("Continue? (Y/N)");
                answer = Console.ReadLine().ToLower();
                Console.Write((answer != "y" && answer != "n") ? "Invalid. " : "");
            } while (answer != "n");
        }
    }
}
