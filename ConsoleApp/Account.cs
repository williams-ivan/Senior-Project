//**************************************************
// File: Account.cs
//
// Purpose: Contains the information for a general
//          account.
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
    class Account
    {
        #region Properties
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "pass")]
        public string Password { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }
        #endregion
    }
}
