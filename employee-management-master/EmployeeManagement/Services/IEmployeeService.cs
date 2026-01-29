using EmployeeManagement.Models;
using EmployeeManagement.DTOs;

namespace EmployeeManagement.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeReadDto>> GetAllAsync();
        Task<EmployeeReadDto?> GetByIdAsync(int id);
        Task<EmployeeReadDto> AddEmployeeAsync(EmployeeCreateDto dto);
        Task<EmployeeReadDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto dto);
        Task DeleteEmployeeAsync(int id);
    }
}
