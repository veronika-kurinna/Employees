﻿using Employees.Server.Data.Entities;
using Employees.Server.Dtos;
using Employees.Server.Dtos.Requests;
using Employees.Server.Models;

namespace Employees.Server.Services
{
    public class EmployeeMapper : IEmployeeMapper
    {
        public Employee MapToModel(EmployeeEntity employee)
        {
            return new Employee()
            {
                Id = employee.Id,
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                Married = employee.Married,
                Phone = employee.Phone,
                Salary = employee.Salary,
            };
        }

        public EmployeeDto MapToDto(Employee employee)
        {
            return new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                Married = employee.Married,
                Phone = employee.Phone,
                Salary = employee.Salary,
            };
        }

        public EmployeeEntity MapToEntity(Employee employee)
        {
            return new EmployeeEntity()
            {
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                Married = employee.Married,
                Phone = employee.Phone,
                Salary = employee.Salary,
            };
        }

        public Employee MapToModel(CreateEmployeeRequest employee)
        {
            return new Employee()
            {
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                Married = employee.Married,
                Phone = employee.Phone,
                Salary = employee.Salary,
            };
        }
    }
}
