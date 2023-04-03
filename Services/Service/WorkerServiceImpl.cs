using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models.DTO;
using HRManagement.Models.Entities;
using HRManagement.Models.Mapper;
using HRManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Services.Service
{
	public class WorkerServiceImpl : IWorkerService
	{

		private HRManagementContext context;
		private WorkerMapper workerMapper;
		private ISalaryService salaryService;

		public WorkerServiceImpl(HRManagementContext HRManagementContext, WorkerMapper mapper, ISalaryService s_service)
		{
			context = HRManagementContext;
			workerMapper = mapper;
			salaryService = s_service;
		}

		public List<WorkerDTO> GetAll()
		{
			List<Worker> workers = (context.Workers?.Include(p => p.Person).Include(p => p.Salary)).ToList();
			return workers.Select(item => workerMapper.mapToWorkerDTO(item)).ToList();
		}

		public WorkerDTO GetWorkerById(long id)
		{
			Worker worker = context.Workers.Include(w => w.Person)
											.Include(w => w.Salary)
											.SingleOrDefault(w => w.WorkerId == id);

			if (worker == null) return null;

			return workerMapper.mapToWorkerDTO(worker);
		}

		public WorkerDTO Save(Worker worker)
		{
			Worker workerCreated = context.Add(worker).Entity;
			context.SaveChanges();
			return workerMapper.mapToWorkerDTO(workerCreated);
		}

		public WorkerDTO Update(long id, Worker worker)
		{
			Worker currentWorker = context.Workers.Find(id);

			if (worker == null) return null;

			currentWorker.Rol = worker.Rol;
			currentWorker.Salary = worker.Salary;

			context.SaveChanges();

			return workerMapper.mapToWorkerDTO(currentWorker);
		}

		public WorkerDTO Delete(long id)
		{
			Worker currentWorker = context.Workers.Find(id);
			Worker workerDeleted = context.Workers.Remove(currentWorker).Entity;
			context.SaveChanges();
			return workerMapper.mapToWorkerDTO(workerDeleted);
		}


		public bool UpdateSalary(long id, DateTime currentDate)
		{

			Worker currentWorker = context.Workers.Include(w => w.Person)
											.Include(w => w.Salary)
											.SingleOrDefault(w => w.WorkerId == id);

			if (currentWorker == null) return false;

			int periods = calculatePeriods(currentDate, currentWorker.WorkingStartDate);

			if (periods == 0) return false;

			Salary previousSalary = currentWorker.Salary;

			Salary newSalary = new Salary();
			newSalary.WorkerId = currentWorker.WorkerId;
			newSalary.SalaryValue = previousSalary.SalaryValue;
			newSalary.SalaryUpdateDate = currentDate;
			
			float currentSalary = currentWorker.Salary!.SalaryValue;

			for (int i = 0; i < periods; i++)
			{
				if (currentWorker.Rol == "worker")
				{
					newSalary.SalaryValue = (float)(newSalary.SalaryValue * 1.05);
				}
				else if (currentWorker.Rol == "specialist")
				{
					newSalary.SalaryValue = (float)(newSalary.SalaryValue * 1.08);
				}
				else if (currentWorker.Rol == "manager")
				{
					newSalary.SalaryValue = (float)(newSalary.SalaryValue * 1.12);
				}
			}

			currentWorker.Salary = newSalary;
			context.Update<Worker>(currentWorker);
			context.SaveChanges();

			return true;
		}

		public int calculatePeriods(DateTime currentDate, DateTime workingStartDate) {
			return (((currentDate.Year - workingStartDate.Year) * 12) + (currentDate.Month - workingStartDate.Month)) / 3;
		}
	}
}