using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}