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
        [DataMember(Name = "first")]
        public string FirstName { get; set; }

        [DataMember(Name = "last")]
        public string LastName { get; set; }

        [DataMember(Name = "nysid")]
        public string NYSID { get; set; }

        [DataMember(Name = "addresses")]
        public ObservableCollection<Address> Addresses { get; set; }
        public Person() {
            Addresses = new ObservableCollection<Address>();
        }
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
