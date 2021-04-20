using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConsoleApp
{
	[DataContract]
	class Customer : Person
    {
		[DataMember(Name = "paymethod")]
		public string PaymentMethod { get; set; }

		[DataMember(Name = "medicalid")]
		public string MedicalCard { get; set; }

		[DataMember(Name = "expdate")]
		public string ExpDate { get; set; }
	}
}
