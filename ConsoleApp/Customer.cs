//**************************************************
// File: Customer.cs
//
// Purpose: Contains the information for a customer
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
using System.Collections.ObjectModel;

namespace ConsoleApp
{
	[DataContract]
	class Customer : Person
    {
        #region Properties
        [DataMember(Name = "paymethod")]
		public string PaymentMethod { get; set; }

		[DataMember(Name = "medicalid")]
		public string MedicalCard { get; set; }

		[DataMember(Name = "expdate")]
		public string ExpDate { get; set; }

		[DataMember(Name = "orders")]
		public ObservableCollection<Order> Orders { get; set; }
		#endregion

		#region Member Methods
		//**************************************************
		// Method: Constructor
		//
		// Purpose: Initializing the Orders property.
		//**************************************************
		public Customer()
		{
			Orders = new ObservableCollection<Order>();
		}
		#endregion
	}
}
