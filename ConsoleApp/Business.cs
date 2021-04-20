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
        public Business()
        {
            Items = new ObservableCollection<MenuItem>();
        }
    }
}
