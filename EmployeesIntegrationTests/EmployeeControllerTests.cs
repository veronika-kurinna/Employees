using Employees.Server;
using Employees.Server.Data;
using Employees.Server.Data.Entities;
using Employees.Server.Dtos.Responses;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace EmployeesIntegrationTests
{
    public class EmployeeControllerTests : IClassFixture<DatabaseFixture<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public EmployeeControllerTests(DatabaseFixture<Program> fixture)
        {
            _factory = fixture;
        }

        [Fact]
        public async Task Get_TwoEmployees_ReturnsTwoEmployees()
        {
            //Arrange
            HttpClient client = _factory.CreateClient();
            EmployeeEntity[] items =
            {
                new EmployeeEntity{ Name = "First employee", DateOfBirth = new DateOnly(), Married = true, Phone = "+380351264567", Salary = 3000 },
                new EmployeeEntity{ Name = "Second employee", DateOfBirth = new DateOnly(), Married = false, Phone = "+380981234667", Salary = 5000 }
            };

            EmployeeContext context = new EmployeeContext(_factory.Options);
            context.Employees.AddRange(items);
            await context.SaveChangesAsync();

            HttpRequestMessage requestGet = new HttpRequestMessage(HttpMethod.Get, $"api/Employee/Get");

            //Act
            HttpResponseMessage responseGet = await client.SendAsync(requestGet);
            string getResponseString = await responseGet.Content.ReadAsStringAsync();
            GetEmployeeResponse? getResponseJson = JsonConvert.DeserializeObject<GetEmployeeResponse>(getResponseString);

            //Assert
            getResponseJson.Employees.Should().Contain(item => item.Id == items[0].Id &&
                                                                item.Name == items[0].Name &&
                                                                item.DateOfBirth == items[0].DateOfBirth &&
                                                                item.Married == items[0].Married &&
                                                                item.Phone == items[0].Phone &&
                                                                item.Salary == items[0].Salary);

            getResponseJson.Employees.Should().Contain(item => item.Id == items[1].Id &&
                                                                item.Name == items[1].Name &&
                                                                item.DateOfBirth == items[1].DateOfBirth &&
                                                                item.Married == items[1].Married &&
                                                                item.Phone == items[1].Phone &&
                                                                item.Salary == items[1].Salary);
        }
    }
}
