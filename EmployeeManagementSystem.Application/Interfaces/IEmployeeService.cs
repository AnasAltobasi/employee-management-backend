using EmployeeManagementSystem.Domain.Dtos;

namespace EmployeeManagementSystem.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetPagedEmployeesAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id);
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreateUpdateDto dto);
        Task<bool> UpdateEmployeeAsync(Guid id, EmployeeCreateUpdateDto dto);
        Task<bool> DeleteEmployeeAsync(Guid id);
    }
}