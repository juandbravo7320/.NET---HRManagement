using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRManagement.Models.Entities
{
    public class Salary
    {
        public long SalaryId {get;set;}
        public long WorkerId {get;set;}
        public DateTime SalaryUpdateDate {get;set;}
        public float SalaryValue {get;set;}
        public bool Active {get;set;}

        [JsonIgnore]
        public virtual Worker? Worker {get;set;}
    }
}