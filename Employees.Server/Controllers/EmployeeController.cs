using Employees.Server.Dtos;
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
    }
}
