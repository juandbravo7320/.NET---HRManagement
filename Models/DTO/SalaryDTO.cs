using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRManagement.Models.DTO
{
	public class SalaryDTO
	{
		public SalaryDTO() { }

		public long SalaryId { get; set; }
		public float SalaryValue { get; set; }
		public DateTime SalaryUpdateDate { get; set; }
	}
}
