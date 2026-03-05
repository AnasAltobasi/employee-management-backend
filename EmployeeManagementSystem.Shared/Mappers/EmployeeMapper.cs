using EmployeeManagementSystem.Domain.Dtos;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Shared.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto MapToDto(this Employee e)
        {
            return new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                Email = e.Email,
                MobileNumber = e.MobileNumber,
                HomeAddress = e.HomeAddress,
                Photo = e.Photo
            };
        }
    }
}