using HRManagement.Models.DTO;
using HRManagement.Models.Entities;
using HRManagement.Models.Mapper;
using HRManagement.Services.Interfaces;
using HRManagement.Utilities;
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

		public List<Worker> GetAll()
		{
			List<Worker> workers = (context.Workers?.Include(p => p.Person)).ToList();

			return workers;
		}

		public WorkerDTO GetWorkerById(long id)
		{
			Worker worker = context.Workers.Include(w => w.Person)
											.SingleOrDefault(w => w.WorkerId == id);

			if (worker == null) return null;

			Salary salary = salaryService.GetByWorkerId(worker.WorkerId);
			worker.Salary = salary.SalaryValue;

			return workerMapper.mapToWorkerDTO(worker);
		}

		public WorkerDTO Save(Worker worker)
		{
			Person person = context.People.Find(worker.PersonId);
			worker.Person = person;
			Worker workerCreated = context.Add(worker).Entity;
			context.SaveChanges();

			Salary newSalary = new Salary();
			newSalary.WorkerId = worker.WorkerId;
			newSalary.SalaryValue = worker.Salary;
			newSalary.Active = true;

			salaryService.Save(newSalary);
			return workerMapper.mapToWorkerDTO(workerCreated);
		}

		public WorkerDTO Update(long id, Worker worker)
		{
			Worker currentWorker = context.Workers.Include(w => w.Person).SingleOrDefault(w => w.WorkerId == id);

			if (worker == null) return null;

			currentWorker.Rol = worker.Rol;

			context.SaveChanges();

			return workerMapper.mapToWorkerDTO(currentWorker);
		}

		public WorkerDTO Delete(long id)
		{
			Worker currentWorker = context.Workers.Include(w => w.Person).SingleOrDefault(w => w.WorkerId == id);

			if (currentWorker == null) return null;

			Worker workerDeleted = context.Workers.Remove(currentWorker).Entity;
			context.SaveChanges();
			return workerMapper.mapToWorkerDTO(workerDeleted);
		}


		public bool UpdateSalary(long id, DateTime currentDate)
		{

			Worker currentWorker = context.Workers.SingleOrDefault(w => w.WorkerId == id);

			if (currentWorker == null) return false;

			if (currentDate < currentWorker.WorkingStartDate) return false;

			TimeSpan diference = currentDate - currentWorker.WorkingStartDate;

			if (diference.TotalDays < 90) return false;

			int periods = calculatePeriods(currentDate, currentWorker.WorkingStartDate);

			if (periods == 0) return false;

			Salary previousSalary = salaryService.GetByWorkerId(id);

			Salary newSalary = new Salary();
			newSalary.WorkerId = currentWorker.WorkerId;
			newSalary.SalaryValue = previousSalary.SalaryValue;
			newSalary.Active = true;

			float currentSalary = previousSalary.SalaryValue;

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

			salaryService.DisableSalary(previousSalary.SalaryId);
			context.Add(newSalary);
			currentWorker.Salary = newSalary.SalaryValue;
			context.SaveChanges();

			return true;
		}

		public int calculatePeriods(DateTime currentDate, DateTime workingStartDate)
		{
			return (((currentDate.Year - workingStartDate.Year) * 12) + (currentDate.Month - workingStartDate.Month)) / 3;
		}

		public MemoryStream GetWorkerCsv()
		{
			List<Worker> workers = (context.Workers?.Include(p => p.Person)).ToList();
			MemoryStream csvStream = Csv.ExportCSV("users", workers);
			return csvStream;
		}
	}
}