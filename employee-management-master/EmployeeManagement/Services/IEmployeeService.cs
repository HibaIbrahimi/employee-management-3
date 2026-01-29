using EmployeeManagement.Models;
using EmployeeManagement.DTOs;

namespace EmployeeManagement.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeReadDto>> GetAllAsync();
        Task<EmployeeReadDto?> GetByIdAsync(Guid id);
        Task<EmployeeReadDto> AddEmployeeAsync(EmployeeCreateDto dto);
        Task<EmployeeReadDto> UpdateEmployeeAsync(Guid id, EmployeeUpdateDto dto);
        Task DeleteEmployeeAsync(Guid id);
    }
}
