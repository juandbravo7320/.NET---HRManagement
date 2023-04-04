using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models.DTO;
using HRManagement.Models.Entities;
using HRManagement.Models.Mapper;
using HRManagement.Services.Interfaces;
using HRManagement.Utilities;

namespace HRManagement.Services.Service
{
	public class SalaryServiceImpl : ISalaryService
	{

		HRManagementContext context;
		SalaryMapper salaryMapper;

		public SalaryServiceImpl(HRManagementContext HRManagementContext, SalaryMapper s_mapper)
		{
			context = HRManagementContext;
			salaryMapper = s_mapper;
		}

		public IEnumerable<Salary> GetAll()
		{
			return context.Salaries;
		}

		public Salary GetSalaryById(long id)
		{
			Salary salary = context.Salaries.Find(id);
			if (salary == null) return null;
			return salary;
		}

		public Salary GetByWorkerId(long id)
		{
			Salary salary = context.Salaries.Where(s => s.Active == true).SingleOrDefault(s => s.WorkerId == id);
			if (salary == null) return null;
			return salary;
		}

		public Salary Save(Salary salary)
		{
			Salary salaryCreated = context.Add(salary).Entity;
			context.SaveChanges();
			return salaryCreated;
		}

		public void DisableSalary(long id)
		{
			Salary salary = GetSalaryById(id);
			if (salary == null) return;
			salary.Active = false;
			context.SaveChanges();
		}

		public MemoryStream GetWorkerHistoricalSalaries(long id)
		{
			List<Salary> salaries = (context.Salaries?.Where(s => s.WorkerId == id).OrderByDescending(s => s.SalaryUpdateDate)).ToList();
			List<SalaryDTO> salariesDTO = salaries.Select(s => salaryMapper.mapToSalaryDTO(s)).ToList();

			MemoryStream csvStream = Csv.ExportCSV("users", salariesDTO);
			return csvStream;
		}
	}
}