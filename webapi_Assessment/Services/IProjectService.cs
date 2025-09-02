using Microsoft.EntityFrameworkCore;
using webapiAssesment.Data;
using webapiAssesment.Interfaces;
using webapiAssesment.Models;

namespace webapiAssesment.Services
{
    public class IProjectService : IProject
    {
        private readonly AppDbContext _context;
        public IProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects.Include(p => p.EmployeeProjects).ThenInclude(e=>e.Employee)
           
                
                .ToListAsync();
        }

        public async Task<Project> AddProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

       



    }
}
