namespace Employees.Server.Dtos.Requests
{
    public class CreateEmployeesRequest
    {
        public IEnumerable<CreateEmployeeRequest> Employees { get; set; }
    }
}
