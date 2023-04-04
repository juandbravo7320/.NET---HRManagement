using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRManagement.Models.Entities
{
	public class Worker
	{
		public long WorkerId { get; set; }
		public long PersonId { get; set; }
		public string? Rol { get; set; }
		public DateTime WorkingStartDate { get; set; }
		public float Salary { get; set; }
		public virtual Person? Person { get; set; }

		[JsonIgnore]
		public virtual ICollection<Salary>? Salaries { get; set; }
	}
}