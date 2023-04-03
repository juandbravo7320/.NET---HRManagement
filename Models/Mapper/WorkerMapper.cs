using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models.DTO;
using HRManagement.Models.Entities;

namespace HRManagement.Models.Mapper
{
    public class WorkerMapper
    {

        private readonly PersonMapper personMapper;
        private readonly SalaryMapper salaryMapper;

        public WorkerMapper(PersonMapper p_mapper, SalaryMapper s_mapper) {
            personMapper = p_mapper;
            salaryMapper = s_mapper;
        }

        public WorkerDTO mapToWorkerDTO (Worker worker) {
            PersonDTO personDTO = personMapper.mapToPersonDTO(worker.Person);
            SalaryDTO salaryDTO = salaryMapper.mapToSalaryDTO(worker.Salary);

            WorkerDTO dto = new WorkerDTO();
            dto.WorkerId = worker.WorkerId;
            dto.Rol = worker.Rol;
            dto.WorkingStartDate = worker.WorkingStartDate;
            dto.Person = personDTO;
            dto.Salary = salaryDTO;

            return dto;
        }
    }
}