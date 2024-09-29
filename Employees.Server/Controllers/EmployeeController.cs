using Employees.Server.Dtos;
using Employees.Server.Dtos.Requests;
using Employees.Server.Dtos.Responses;
using Employees.Server.Models;
using Employees.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeMapper _mapper;
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeMapper mapper, IEmployeeService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<GetEmployeeResponse> Get()
        {
            IEnumerable<Employee> employees = await _service.GetAll();
            IEnumerable<EmployeeDto> employeeDto = employees.Select(employee => _mapper.MapToDto(employee));
            return new GetEmployeeResponse()
            {
                Employees = employeeDto
            };
        }

        [HttpPost]
        public async Task CreateEmployees([FromBody] CreateEmployeesRequest request)
        {
            IEnumerable<Employee> employees = request.Employees.Select(employee => _mapper.MapToModel(employee));
            await _service.CreateEmployees(employees);
        }

        [HttpPut("{id:int}")]
        public async Task Update(int id, UpdateEmployeeRequest request)
        {
            await _service.UpdateEmployee(id, request.Name, request.DateOfBirth, request.Married, request.Phone, request.Salary);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _service.DeleteEmployee(id);
        }
    }
}
