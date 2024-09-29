using System.ComponentModel.DataAnnotations;

namespace Employees.Server.Dtos.Requests
{
    public class UpdateEmployeeRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        public bool Married { get; set; }

        [Required]
        [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Phone number must be a valid international phone number")]
        public string Phone { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}
