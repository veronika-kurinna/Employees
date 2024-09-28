using Employees.Server.Data.Repositories;
using Employees.Server.Models;

namespace Employees.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _repository.Get();
        }
    }
}
