using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapiAssesment.Interfaces;
using webapiAssesment.Models;

namespace webapiAssesment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class departmentController : ControllerBase
    {
        private readonly IDepartment _service;
            public departmentController(IDepartment service)
            {
                _service = service;
            }
            [HttpGet]
            public async Task<IActionResult> GetAllDept()
            {
                var res = await _service.GetAllDepartments();
                return Ok(res);
            }
            [HttpPost]
            public async Task<ActionResult> AddDept(Department dept)
            {
                var res = await _service.AddDepartment(dept);
                return Ok(res);
            }
            [HttpPut("id")]
            public async Task<ActionResult> UpdateDept(int id, Department dept)
            {
                var res = await _service.UpdateDepartment(id, dept);
                return Ok(res);
            }
        }
    }
