using HRManagement.Models.Entities;
using HRManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private IPersonService personService;

        public PersonController(IPersonService service) {
            personService = service;
        }

        [HttpGet]
        public IActionResult GetAll() {
            return Ok(personService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetPersonById(long id) {
            return Ok(personService.GetPersonById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person) {
            return Ok(personService.Save(person));
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Person person) {
            return Ok(personService.Update(id, person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            return Ok(personService.Delete(id));
        }
    }
}