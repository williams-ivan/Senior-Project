using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConsoleApp
{
    [DataContract]
    class MenuItem
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "info")]
        public string Information { get; set; }

        [DataMember(Name = "presc")]
        public string PrescriptionLength { get; set; }

        [DataMember(Name = "price")]
        public string Price { get; set; }
    }
}
