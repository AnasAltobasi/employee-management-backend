using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Domain.Dtos;
using EmployeeManagementSystem.Shared.Mappers;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id)
        {
            var emp = await _context.Employees.FindAsync(id);
            return emp?.MapToDto();
        }

        public async Task<IEnumerable<EmployeeDto>> GetPagedEmployeesAsync(int pageNumber, int pageSize)
        {
            var employees = await _context.Employees
                .OrderBy(e => e.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return employees.Select(e => e.MapToDto());
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreateUpdateDto dto)
        {
            var entity = new Employee
            {
                EmployeeId = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                MobileNumber = dto.MobileNumber,
                HomeAddress = dto.HomeAddress,
                Photo = dto.Photo
            };

            _context.Employees.Add(entity);
            await _context.SaveChangesAsync();

            return new EmployeeDto
            {
                EmployeeId = entity.EmployeeId,
                Name = entity.Name,
                Email = entity.Email,
                MobileNumber = entity.MobileNumber,
                HomeAddress = entity.HomeAddress,
                Photo = entity.Photo
            };
        }

        public async Task<bool> UpdateEmployeeAsync(Guid id, EmployeeCreateUpdateDto dto)
        {
            var existing = await _context.Employees.FindAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.MobileNumber = dto.MobileNumber;
            existing.HomeAddress = dto.HomeAddress;
            existing.Photo = dto.Photo;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetTotalCountAsync() => await _context.Employees.CountAsync();

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return false;
            _context.Employees.Remove(emp);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}