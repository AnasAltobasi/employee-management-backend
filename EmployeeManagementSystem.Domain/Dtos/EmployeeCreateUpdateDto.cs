namespace EmployeeManagementSystem.Domain.Dtos
{
    public class EmployeeCreateUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string HomeAddress { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
    }
}