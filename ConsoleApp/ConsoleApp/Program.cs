using System;
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
        static DataList dataStore;
        static void save() {
            FileStream writer = new FileStream(filename, FileMode.Create, FileAccess.Write);

            DataContractJsonSerializer ser;
            ser = new DataContractJsonSerializer(typeof(DataList));

            ser.WriteObject(writer, dataStore);
            writer.Dispose();
        }
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
        static void cRegister() {
            Customer c = new Customer();
            string[] args = { "false" };
            string choice = "y";
            do {
                PropertyInfo[] properties = typeof(Customer).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name != "Addresses")
                    {
                        Console.Write(property.Name + ": ");
                        property.SetValue(c, Console.ReadLine());
                    }
                }
                if (check("c", c.Email, args)) {
                    Console.Write("An account was already made with this email.");
                    do {
                        Console.WriteLine(" Continue? (Y/N)");
                        choice = Console.ReadLine();
                        Console.Write((choice.ToLower() != "y" && choice.ToLower() != "n") ? "Invalid." : "");
                    } while (choice.ToLower() != "y" && choice.ToLower() != "n");
                    choice = Console.ReadLine();
                }
            } while (check("c", c.Email, args) && choice.ToLower() == "y");
            if (choice.ToLower() == "y") {
                dataStore.Customers.Add(c);
            }
        }
        static void bRegister()
        {
            Business b = new Business();
            string[] args = { "false" };
            string choice = "y";
            do {
                PropertyInfo[] properties = typeof(Business).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name != "Address")
                    {
                        Console.WriteLine("Address:");
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
                    if (check("b", b.Email, args))
                    {
                        Console.Write("An account was already made with this email.");
                        do
                        {
                            Console.WriteLine(" Continue? (Y/N)");
                            choice = Console.ReadLine();
                            Console.Write((choice.ToLower() != "y" && choice.ToLower() != "n") ? "Invalid." : "");
                        } while (choice.ToLower() != "y" && choice.ToLower() != "n");
                        choice = Console.ReadLine();
                    }
                }
            } while (check("b", b.Email, args) && choice.ToLower() == "y");
            if (choice.ToLower() == "y")
            {
                dataStore.Businesses.Add(b);
            }
        }
        static void dRegister()
        {
            Person d = new Person();
            string[] args = { "false" };
            string choice = "y";
            do {
                PropertyInfo[] properties = typeof(Person).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name != "Addresses")
                    {
                        Console.Write(property.Name + ": ");
                        property.SetValue(d, Console.ReadLine());
                    }
                }
                if (check("d", d.Email, args))
                {
                    Console.Write("An account was already made with this email.");
                    do
                    {
                        Console.WriteLine(" Continue? (Y/N)");
                        choice = Console.ReadLine();
                        Console.Write((choice.ToLower() != "y" && choice.ToLower() != "n") ? "Invalid." : "");
                    } while (choice.ToLower() != "y" && choice.ToLower() != "n");
                    choice = Console.ReadLine();
                }
            } while (check("d", d.Email, args) && choice.ToLower() == "y");
            if (choice.ToLower() == "y")
            {
                dataStore.Dashers.Add(d);
            }
        }
        static void register() {
            string choice;
            do
            {
                Console.WriteLine("(C)ustomer, (B)usiness, or (D)asher?");
                choice = Console.ReadLine();
                Console.Write((choice.ToLower() != "b" && choice.ToLower() != "c" && choice.ToLower() != "d") ? "Invalid. " : "");
            } while (choice.ToLower() != "b" && choice.ToLower() != "c" && choice.ToLower() != "d");

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
        static void login() {
            string email, password = "", choice = "y";
            string[] args = { "true", password };
            do {
                Console.Write("Email: ");
                email = Console.ReadLine();
                Console.Write("Password: ");
                password = Console.ReadLine();
                if (!check("c", email, args))
                {
                    Console.Write("Invalid user.");
                    do
                    {
                        Console.WriteLine(" Continue? (Y/N)");
                        choice = Console.ReadLine();
                        Console.Write((choice.ToLower() != "y" && choice.ToLower() != "n") ? "Invalid." : "");
                    } while (choice.ToLower() != "y" && choice.ToLower() != "n");
                    choice = Console.ReadLine();
                }
            } while (!check("c", email, args) && choice.ToLower() == "y");
            if (choice.ToLower() == "y")
            {
                Console.WriteLine("Valid user.");
            }
        }
        static void Main(string[] args)
        {
            FileStream reader = new FileStream(filename, FileMode.Open, FileAccess.Read);
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

            string choice;

            do {
                Console.WriteLine("(L)ogin or (R)egister?");
                choice = Console.ReadLine();
                Console.Write((choice.ToLower() != "l" && choice.ToLower() != "r") ? "Invalid. " : "");
            } while (choice.ToLower() != "l" && choice.ToLower() != "r");

            switch (choice) {
                case "r":
                    register();
                    break;
                case "l":
                    login();
                    break;
            }
        }
    }
}
