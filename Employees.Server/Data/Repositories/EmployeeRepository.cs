using Employees.Server.Data.Entities;
using Employees.Server.Models;
using Employees.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Employees.Server.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;
        private readonly IEmployeeMapper _mapper;

        public EmployeeRepository(EmployeeContext context, IEmployeeMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            IEnumerable<EmployeeEntity> employees = await _context.Employees.ToListAsync();
            return employees.Select(employee => _mapper.MapToModel(employee));
        }

        public async Task<Employee> GetById(int id)
        {
            EmployeeEntity? employee = await _context.Employees.Where(i => i.Id == id)
                                                               .FirstOrDefaultAsync();
            if (employee == null)
            {
                throw new ArgumentException($"Item with id {id} doesn't exist");
            }
            return _mapper.MapToModel(employee);
        }

        public async Task CreateEmployees(IEnumerable<Employee> employees)
        {
            IEnumerable<EmployeeEntity> employeesToCreate = employees.Select(employee => _mapper.MapToEntity(employee));
            _context.AddRange(employeesToCreate);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Employee employee)
        {
            EmployeeEntity? employeeToUpdate = await _context.Employees.Where(i => i.Id == employee.Id)
                                                                       .FirstOrDefaultAsync();
            if (employeeToUpdate == null)
            {
                throw new ArgumentException($"Employee with id {employee.Id} doesn't exist");
            }

            employeeToUpdate.Name = employee.Name;
            employeeToUpdate.DateOfBirth = employee.DateOfBirth;
            employeeToUpdate.Married = employee.Married;
            employeeToUpdate.Phone = employee.Phone;
            employeeToUpdate.Salary = employee.Salary;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            EmployeeEntity? employeeToDelete = await _context.Employees.Where(i => i.Id == id)
                                                                       .FirstOrDefaultAsync();
            if (employeeToDelete == null)
            {
                throw new ArgumentException($"Employee with id {id} doesn't exist");
            }
            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
