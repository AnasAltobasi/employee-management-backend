using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Domain.Dtos
{
    public class EmployeeDto
    {
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]

        [EmailAddress(ErrorMessage = "Invalid email format")] 
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required")]
        [Phone]
        public string MobileNumber { get; set; } = string.Empty; 

        public string HomeAddress { get; set; } = string.Empty; 

        public string Photo { get; set; } = string.Empty;
    }
}