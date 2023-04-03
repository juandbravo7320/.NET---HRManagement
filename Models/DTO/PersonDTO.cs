using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models.Entities;

namespace HRManagement.Models.DTO
{
	public class PersonDTO
	{

		public PersonDTO() { }
		public long PersonId { get; set; }
		public string? Name { get; set; }
		public string? Lastname { get; set; }
		public string? Email { get; set; }
		public string? PersonalAddress { get; set; }
		public int Phone { get; set; }
		public byte[]? Picture { get; set; }
		public ICollection<Worker>? Workers { get; set; }
	}
}