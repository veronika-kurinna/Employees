using Employees.Server.Models;

namespace Employees.Server.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Get();
    }
}
