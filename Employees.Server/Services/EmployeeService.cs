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

        public async Task CreateEmployees(IEnumerable<Employee> employees)
        {
            await _repository.CreateEmployees(employees);
        }
        public async Task UpdateEmployee(int id, string name, DateOnly dateOfBirth, bool married, string phone, decimal salary)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Id {id} is invalid. Id must be more than zero");
            }

            Employee employee = await _repository.GetById(id);

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(phone))
            {
                employee.Name = name;
                employee.DateOfBirth = dateOfBirth;
                employee.Married = married;
                employee.Phone = phone;
                employee.Salary = salary;
            }
            await _repository.Update(employee);
        }

        public async Task DeleteEmployee(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Id {id} is invalid. Id must be more than zero");
            }
            await _repository.Delete(id);
        }
    }
}
