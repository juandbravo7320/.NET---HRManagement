using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models.Entities;
using HRManagement.Services.Interfaces;

namespace HRManagement.Services.Service
{
	public class SalaryServiceImpl : ISalaryService
	{

		HRManagementContext context;

		public SalaryServiceImpl(HRManagementContext HRManagementContext)
		{
			context = HRManagementContext;
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
	}
}