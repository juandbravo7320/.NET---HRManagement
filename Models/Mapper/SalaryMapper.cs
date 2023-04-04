using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models.DTO;
using HRManagement.Models.Entities;

namespace HRManagement.Models.Mapper
{
    public class SalaryMapper
    {

        public SalaryMapper() { }

        public SalaryDTO mapToSalaryDTO (Salary salary) {
            SalaryDTO dto = new SalaryDTO();

            dto.SalaryId = salary.SalaryId;
            dto.SalaryValue = salary.SalaryValue;
            dto.SalaryUpdateDate = salary.SalaryUpdateDate;

            return dto;
        }
    }
}