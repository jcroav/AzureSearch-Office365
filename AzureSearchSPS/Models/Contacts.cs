using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureSearchSPS.Models
{
    public class Contacts
    {
        public string ContactId { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
    }
}