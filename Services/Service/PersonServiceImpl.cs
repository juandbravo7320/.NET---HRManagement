using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models.Entities;
using HRManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Services
{
	public class PersonServiceImpl : IPersonService
	{

		private HRManagementContext context;

		public PersonServiceImpl(HRManagementContext HRManagementContext) {
			context = HRManagementContext;
		}

		public IEnumerable<Person> GetAll()
		{
			return context.People?.Include(p => p.Workers);
		}

		public Person GetPersonById(long id)
		{
			Person person = context.People.Include(p => p.Workers)
											.SingleOrDefault(p => p.PersonId == id);

			if (person == null) return null;

			return person;
		}

		public Person Save(Person person)
		{
			Person personCreated = context.Add(person).Entity;
			context.SaveChanges();
			return person;
		}

		public Person Update(long id, Person person)
		{
			Person personFinded =  context.People.Find(id);
			personFinded.Name = person.Name;
			personFinded.Lastname = person.Lastname;
			personFinded.PersonalAddress = person.PersonalAddress;
			personFinded.Email = person.Email;
			personFinded.Phone = person.Phone;
			if (person.Picture != null) personFinded.Picture = person.Picture;

			context.SaveChanges();

			return personFinded;
		}

		public Person Delete(long id)
		{
			Person personFinded =  context.People.Find(id);
			Person personDeleted = context.People.Remove(personFinded).Entity;
			context.SaveChanges();
			return personDeleted;
		}
	}
}