using Microsoft.EntityFrameworkCore;
using webapiAssesment.Data;
using webapiAssesment.Interfaces;
using webapiAssesment.Models;

namespace webapiAssesment.Services
{
    public class IDepartmentservice : IDepartment
    {
        private readonly AppDbContext _context;
        public IDepartmentservice(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Departments.Include(d => d.Employees)
                .ToListAsync();
        }

        public async Task<Department> AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department?> UpdateDepartment(int id, Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(id);
            if (existingDepartment == null)
            {
                return null;
            }
            existingDepartment.Name = department.Name;
            await _context.SaveChangesAsync();
            return existingDepartment;
        }
    }
}
