using Employees.Server.Models;

namespace Employees.Server.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAll();
    }
}
