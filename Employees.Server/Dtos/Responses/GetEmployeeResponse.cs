namespace Employees.Server.Dtos.Responses
{
    public class GetEmployeeResponse
    {
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}
