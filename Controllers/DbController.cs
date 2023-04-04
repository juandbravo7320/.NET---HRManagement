using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DbController : ControllerBase
    {
        HRManagementContext dbContext;

        public DbController(HRManagementContext context) {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult CreateDatabase() {
            return Ok(dbContext.Database.EnsureCreated());
        }
    }
}