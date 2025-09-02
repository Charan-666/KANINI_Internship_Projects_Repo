using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapiAssesment.Interfaces;
using webapiAssesment.Models;

namespace webapiAssesment.Controllers
{
   

        [Route("api/[controller]")]
        [ApiController]
        public class ProjectController : ControllerBase
        {
            private readonly IProject _service;
            public ProjectController(IProject service)
            {
                _service = service;
            }
            [HttpGet]
            public async Task<IActionResult> GetAllProjects()
            {
                var res = await _service.GetAllProjects();
                return Ok(res);
            }
            [HttpPost]
            public async Task<ActionResult> AddProject(Project project)
            {
                var res = await _service.AddProject(project);
                return Ok(res);
            }
        }
    }
