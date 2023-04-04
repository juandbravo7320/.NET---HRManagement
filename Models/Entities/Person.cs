using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRManagement.Models.Entities
{
    public class Person
    {
        public long PersonId {get;set;}
        public string? Name {get;set;}
        public string? Lastname {get;set;}
        public string? Email {get;set;}
        public string? PersonalAddress {get;set;}
        public int Phone {get;set;}
        public byte[]? Picture {get;set;}

        [JsonIgnore]
        public virtual ICollection<Worker>? Workers {get;set;}
    }
}