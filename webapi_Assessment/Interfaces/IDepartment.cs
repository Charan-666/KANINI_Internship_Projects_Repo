using webapiAssesment.Models;

namespace webapiAssesment.Interfaces
{
    public interface IDepartment
    {
        public Task<IEnumerable<Department>> GetAllDepartments();

        public Task<Department> AddDepartment(Department department);

        public Task<Department?> UpdateDepartment(int id, Department department);


    }
}
