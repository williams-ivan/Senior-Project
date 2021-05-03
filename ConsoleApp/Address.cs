//**************************************************
// File: Address.cs
//
// Purpose: Contains the information for an address.
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
using System.Runtime.Serialization;

namespace ConsoleApp
{
    [DataContract]
    class Address
    {
        #region Properties
        [DataMember(Name = "street")]
        public string StreetAddress { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "zip")]
        public string ZipCode { get; set; }
        #endregion

        #region Member Methods
        //**************************************************
        // Method: ToString
        //
        // Purpose: Converting an Address object to a
        //          string.
        //**************************************************
        public override string ToString()
        {
            return StreetAddress + "\n\t" + City + ", " + State + "\t" + ZipCode;
        }
        #endregion
    }
}
