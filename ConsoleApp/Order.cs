//**************************************************
// File: Order.cs
//
// Purpose: Contains the information for an order.
//
// Written By: Ivan Williams
//
// Compiler: Visual Studio 2019
//**************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [DataContract]
    class Order
    {
        #region Properties
        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "price")]
        public string TotalPrice { get; set; }

        [DataMember(Name = "share")]
        public string DasherShare { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "business")]
        public Business Business { get; set; }

        [DataMember(Name = "dasher")]
        public Person Dasher { get; set; }

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
        public Order()
        {
            Items = new ObservableCollection<MenuItem>();
        }
        #endregion
    }
}
