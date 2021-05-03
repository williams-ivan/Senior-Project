//**************************************************
// File: Business.cs
//
// Purpose: Contains the information for a business
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
    class Business : Account
    {
        #region Properties
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "hours")]
        public string Hours { get; set; }

        [DataMember(Name = "address")]
        public Address Address { get; set; }

        [DataMember(Name = "items")]
        public ObservableCollection<MenuItem> Items { get; set; }
        #endregion

        #region Member Methods
        //**************************************************
        // Method: Constructor
        //
        // Purpose: Initializing the Items property.
        //**************************************************
        public Business()
        {
            Items = new ObservableCollection<MenuItem>();
        }

        //**************************************************
        // Method: ToString
        //
        // Purpose: Converting a Business object to a
        //          string.
        //**************************************************
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
