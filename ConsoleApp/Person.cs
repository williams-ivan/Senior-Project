//**************************************************
// File: Person.cs
//
// Purpose: Contains the information for a person's
//          account.
//
// Written By: Ivan Williams
//
// Compiler: Visual Studio 2019
//**************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConsoleApp
{
    [DataContract]
    class Person : Account
    {
        #region Properties
        [DataMember(Name = "first")]
        public string FirstName { get; set; }

        [DataMember(Name = "last")]
        public string LastName { get; set; }

        [DataMember(Name = "nysid")]
        public string NYSID { get; set; }

        [DataMember(Name = "addresses")]
        public ObservableCollection<Address> Addresses { get; set; }
        #endregion

        #region Member Methods
        //**************************************************
        // Method: Constructor
        //
        // Purpose: Initializing the Addresses property.
        //**************************************************
        public Person() {
            Addresses = new ObservableCollection<Address>();
        }

        //**************************************************
        // Method: ToString
        //
        // Purpose: Converting a Person object to a string.
        //**************************************************
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
        #endregion
    }
}
