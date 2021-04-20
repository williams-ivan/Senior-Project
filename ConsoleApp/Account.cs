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
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "pass")]
        public string Password { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }
    }
}
