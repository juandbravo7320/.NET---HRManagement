using HRManagement.Models.Entities;
using HRManagement.Models.DTO;

namespace HRManagement.Services.Interfaces
{
    public interface IWorkerService
    {
        List<WorkerDTO> GetAll();
        WorkerDTO GetWorkerById(long id);
        WorkerDTO Save(Worker worker);
        WorkerDTO Update(long id, Worker worker);
        WorkerDTO Delete(long id);
        bool UpdateSalary(long id, DateTime date);
    }
}