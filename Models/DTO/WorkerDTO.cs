using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models.Entities;

namespace HRManagement.Models.DTO
{
    public class WorkerDTO
    {

        public WorkerDTO() {}

        public long WorkerId {get;set;}
        public string? Rol {get;set;}
        public SalaryDTO Salary {get;set;}
        public DateTime WorkingStartDate {get;set;}
        public PersonDTO Person {get;set;}
    }
}