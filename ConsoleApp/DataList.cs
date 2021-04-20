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
    class DataList
    {
        [DataMember(Name = "customers")]
        public ObservableCollection<Customer> Customers { get; set; }

        [DataMember(Name = "dashers")]
        public ObservableCollection<Person> Dashers { get; set; }

        [DataMember(Name = "businesses")]
        public ObservableCollection<Business> Businesses { get; set; }

        [DataMember(Name = "accounts")]
        public ObservableCollection<Account> Accounts { get; set; }
    }
}
