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
    }
}
