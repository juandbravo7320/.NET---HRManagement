using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using HRManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {

        private readonly ISalaryService salaryService;

        public SalaryController(ISalaryService service) {
            salaryService = service;
        }

        [HttpGet]
        public IActionResult GetAll() {
            return Ok(salaryService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id) {
            return Ok(salaryService.GetSalaryById(id));
        }

        [HttpGet("historicalSalaries/{id}")]
        public IActionResult GetWorkerHistoricalSalaries(long id) {
            return Ok(salaryService.GetWorkerHistoricalSalaries(id));
        }

        [HttpGet("historicalSalariesCSV/{id}")]
		public IActionResult GetCSVWorker(long id)
		{
			MemoryStream csvStream = salaryService.GetWorkerHistoricalSalaries(id);

			var contentDisposition = new ContentDisposition
			{
				FileName = "historicalSalaries.csv",
				Inline = false
			};

			Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

			return File(csvStream, "text/csv", "historicalSalaries.csv");
		}
    }
}