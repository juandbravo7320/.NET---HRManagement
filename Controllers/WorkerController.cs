using System.Net.Mime;
using System.Text;
using HRManagement.Models.DTO;
using HRManagement.Models.Entities;
using HRManagement.Services.Interfaces;
using HRManagement.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WorkerController : ControllerBase
	{
		private IWorkerService workerService;

		public WorkerController(IWorkerService service)
		{
			workerService = service;
		}

		[HttpGet]
		public IActionResult GetAllworkers()
		{
			return Ok(workerService.GetAll());
		}

		[HttpGet("{id}")]
		public IActionResult GetWorkerById(long id)
		{
			return Ok(workerService.GetWorkerById(id));
		}

		[HttpPost]
		public IActionResult Post([FromBody] Worker worker)
		{
			return Ok(workerService.Save(worker));
		}

		[HttpPut("{id}")]
		public IActionResult Put(long id, [FromBody] Worker worker)
		{
			return Ok(workerService.Update(id, worker));
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
		{
			return Ok(workerService.Delete(id));
		}

		[HttpGet("updateSalary/{id}/{date:datetime}")]
		public IActionResult UpdateSalary(long id, DateTime date)
		{
			return Ok(workerService.UpdateSalary(id, date));
		}

		[HttpGet("workersCSV")]
		public IActionResult GetCSVWorker()
		{
			MemoryStream csvStream = workerService.GetWorkerCsv();

			var contentDisposition = new ContentDisposition
			{
				FileName = "users.csv",
				Inline = false
			};

			Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

			return File(csvStream, "text/csv", "users.csv");
		}
	}
}