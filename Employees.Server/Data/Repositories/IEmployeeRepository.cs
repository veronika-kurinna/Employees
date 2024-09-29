using Employees.Server.Models;

namespace Employees.Server.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Get();
        Task<Employee> GetById(int id);
        Task CreateEmployees(IEnumerable<Employee> employees);
        Task Update(Employee employee);
        Task Delete(int id);
    }
}
