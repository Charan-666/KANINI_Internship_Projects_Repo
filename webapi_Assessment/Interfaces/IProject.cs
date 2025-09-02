using webapiAssesment.Models;

namespace webapiAssesment.Interfaces
{
    public interface IProject
    {
        public Task<IEnumerable<Project>> GetAllProjects();
        
        public Task<Project> AddProject(Project project);
       
       
    }
}
