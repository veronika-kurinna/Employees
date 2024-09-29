using Employees.Server.Models;

namespace Employees.Server.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAll();
        Task CreateEmployees(IEnumerable<Employee> employees);
        Task UpdateEmployee(int id, string name, DateOnly dateOfBirth, bool married, string phone, decimal salary);
        Task DeleteEmployee(int id);
    }
}
