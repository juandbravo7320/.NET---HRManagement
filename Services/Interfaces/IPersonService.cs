using HRManagement.Models.Entities;

namespace HRManagement.Services.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAll();
        Person GetPersonById(long id);
        Person Save(Person person);
        Person Update(long id, Person person);
        Person Delete(long id);
    }
}