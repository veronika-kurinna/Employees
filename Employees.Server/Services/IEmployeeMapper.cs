using Employees.Server.Data.Entities;
using Employees.Server.Dtos;
using Employees.Server.Dtos.Requests;
using Employees.Server.Models;

namespace Employees.Server.Services
{
    public interface IEmployeeMapper
    {
        Employee MapToModel(EmployeeEntity employee);
        EmployeeDto MapToDto(Employee employee);
        EmployeeEntity MapToEntity(Employee employee);
        Employee MapToModel(CreateEmployeeRequest employees);
    }
}
