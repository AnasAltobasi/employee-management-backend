using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Dtos;

namespace EmployeeManagementSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly string _logPath;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

            // Set up logging path in the project root
            _logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "employee_api_logs.txt");

            if (!Directory.Exists(Path.GetDirectoryName(_logPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_logPath)!);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                LogToFile("GET ALL", $"Request: Page={pageNumber}, Size={pageSize}");

                var employees = await _employeeService.GetPagedEmployeesAsync(pageNumber, pageSize);
                var totalCount = await _employeeService.GetTotalCountAsync();

                var response = new
                {
                    TotalRecords = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = employees
                };

                LogToFile("GET ALL", "Response: Success - Returned paged data");
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogToFile("GET ALL", $"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching employees.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                LogToFile("GET BY ID", $"Request: ID={id}");
                var employee = await _employeeService.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    LogToFile("GET BY ID", "Response: Not Found");
                    return NotFound();
                }

                LogToFile("GET BY ID", "Response: Success");
                return Ok(employee);
            }
            catch (Exception ex)
            {
                LogToFile("GET BY ID", $"Error: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateUpdateDto dto)
        {
            try
            {
                var result = await _employeeService.CreateEmployeeAsync(dto);
                LogToFile("POST", $"Created: {result.EmployeeId}");
                return CreatedAtAction(nameof(GetById), new { id = result.EmployeeId }, result);
            }
            catch (Exception ex)
            {
                LogToFile("POST", $"Error: {ex.Message}");
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeCreateUpdateDto dto)
        {
            try
            {
                var updated = await _employeeService.UpdateEmployeeAsync(id, dto);
                if (!updated) return NotFound();

                LogToFile("PUT", $"Updated ID: {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                LogToFile("PUT", $"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                LogToFile("DELETE", $"Request: ID={id}");

                var deleted = await _employeeService.DeleteEmployeeAsync(id);
                if (!deleted)
                {
                    LogToFile("DELETE", "Response: Not Found");
                    return NotFound();
                }

                LogToFile("DELETE", "Response: Success");
                return NoContent();
            }
            catch (Exception ex)
            {
                LogToFile("DELETE", $"Error: {ex.Message}");
                return StatusCode(500, "Error deleting record.");
            }
        }

        private void LogToFile(string action, string message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | [{action}] | {message}{Environment.NewLine}";
            System.IO.File.AppendAllText(_logPath, logEntry);
        }
    }
}