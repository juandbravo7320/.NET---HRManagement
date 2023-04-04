using HRManagement.Models.Entities;
using HRManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Services.Interfaces
{
    public interface IWorkerService
    {
        List<Worker> GetAll();
        WorkerDTO GetWorkerById(long id);
        WorkerDTO Save(Worker worker);
        WorkerDTO Update(long id, Worker worker);
        WorkerDTO Delete(long id);
        bool UpdateSalary(long id, DateTime date);
        MemoryStream GetWorkerCsv();
    }
}