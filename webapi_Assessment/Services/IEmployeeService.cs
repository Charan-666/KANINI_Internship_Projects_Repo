using Microsoft.EntityFrameworkCore;
using webapiAssesment.Data;
using webapiAssesment.Interfaces;
using webapiAssesment.Models;


namespace webapiAssesment.Services
{
    public class IEmployeeService : IEmployee
    {
        private readonly AppDbContext _context;
        public IEmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees() 
        {
            return await _context.Employees.Include(d=>d.Department).ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id) 
        {
            var employee = await _context.Employees.FindAsync(id);
          
            return employee;
        }

        public async Task<Employee> AddEmployee(Employee employee) 
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateEmployee(int id, Employee employee) 
        {
            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return null;
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email;
            existingEmployee.Role = employee.Role;
            existingEmployee.DepartmentId = employee.DepartmentId;
            await _context.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

