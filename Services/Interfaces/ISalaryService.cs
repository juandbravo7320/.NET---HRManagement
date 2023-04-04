using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models.Entities;

namespace HRManagement.Services.Interfaces
{
    public interface ISalaryService
    {
        IEnumerable<Salary> GetAll();

        Salary GetSalaryById(long id);

        Salary GetByWorkerId(long id);

        Salary Save(Salary salary);

        void DisableSalary(long id);
    }
}