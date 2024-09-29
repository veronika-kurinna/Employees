using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employees.Server.Migrations
{
    /// <inheritdoc />
    public partial class FillTableEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Employee (Name, DateOfBirth, Married, Phone, Salary) VALUES ('Alice', '1998-11-11', 0, '+380351264567', 3000)");
            migrationBuilder.Sql("INSERT INTO Employee (Name, DateOfBirth, Married, Phone, Salary) VALUES ('Ivan', '1989-03-23', 0, '+380441234569', 5000)");
            migrationBuilder.Sql("INSERT INTO Employee (Name, DateOfBirth, Married, Phone, Salary) VALUES ('Julia', '1991-09-18', 1, '+380914234556', 6000)");
            migrationBuilder.Sql("INSERT INTO Employee (Name, DateOfBirth, Married, Phone, Salary) VALUES ('Alex', '1993-01-29', 0, '+380981234667', 4000)");
            migrationBuilder.Sql("INSERT INTO Employee (Name, DateOfBirth, Married, Phone, Salary) VALUES ('Kate', '2000-05-10', 1, '+380311234587', 1000)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
