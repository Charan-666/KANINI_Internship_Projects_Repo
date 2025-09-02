using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapiAssesment.Interfaces;
using webapiAssesment.Services;

namespace webapiAssesment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _ser;
        public EmployeeController(IEmployee ser) {
            _ser = ser;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _ser.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _ser.GetEmployeeById(id);
           if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Models.Employee employee)
        {
            var res = await _ser.AddEmployee(employee);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Models.Employee employee)
        {
            var updatedEmployee = await _ser.UpdateEmployee(id, employee);
            if (updatedEmployee == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _ser.DeleteEmployee(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }



    }
}


