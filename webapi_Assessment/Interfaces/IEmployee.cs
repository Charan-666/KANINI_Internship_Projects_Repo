using webapiAssesment.Models;

namespace webapiAssesment.Interfaces
{
    public interface IEmployee
    {
        public Task<IEnumerable<Employee>> GetAllEmployees();

        public Task<Employee?> GetEmployeeById(int id);

        public Task<Employee> AddEmployee(Employee employee);

        public Task<Employee?> UpdateEmployee(int id, Employee employee);

        public Task<bool> DeleteEmployee(int id);

    }
}
