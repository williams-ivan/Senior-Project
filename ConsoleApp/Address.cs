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
        public Address() { }

        [DataMember(Name = "aptnum")]
        public string AptNumber { get; set; }

        [DataMember(Name = "street")]
        public string StreetAddress { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "zip")]
        public string ZipCode { get; set; }
    }
}
