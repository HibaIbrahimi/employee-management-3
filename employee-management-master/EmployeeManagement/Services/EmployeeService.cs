using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services
{
    // Service = couche métier : validations métier + mapping Entity <-> DTO
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // Mapper Entity -> ReadDto
        private static EmployeeReadDto MapToReadDto(Employee employee)
        {
            return new EmployeeReadDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Position = employee.Position
            };
        }

        // Mapper CreateDto -> Entity
        private static Employee MapCreateDtoToEntity(EmployeeCreateDto dto)
        {
            return new Employee
            {
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                Email = dto.Email.Trim(),
                Phone = dto.Phone.Trim(),
                Position = dto.Position.Trim()
            };
        }

        public async Task<IEnumerable<EmployeeReadDto>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();
            return employees.Select(MapToReadDto);
        }

        public async Task<EmployeeReadDto?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return null;

            return MapToReadDto(employee);
        }

        public async Task<EmployeeReadDto> AddEmployeeAsync(EmployeeCreateDto dto)
        {
            var employee = MapCreateDtoToEntity(dto);

            // Générer l'Id côté backend
            employee.Id = Guid.NewGuid();

            await _repository.AddEmployeeAsync(employee);

            return MapToReadDto(employee);
        }

        public async Task<EmployeeReadDto> UpdateEmployeeAsync(Guid id, EmployeeUpdateDto dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id de l'employé invalide.", nameof(id));

            var existingEmployee = await _repository.GetByIdAsync(id);
            if (existingEmployee == null)
                throw new KeyNotFoundException($"Employé avec l'id {id} introuvable.");

            existingEmployee.FirstName = dto.FirstName.Trim();
            existingEmployee.LastName = dto.LastName.Trim();
            existingEmployee.Email = dto.Email.Trim();
            existingEmployee.Phone = dto.Phone.Trim();
            existingEmployee.Position = dto.Position.Trim();

            await _repository.UpdateEmployeeAsync(existingEmployee);

            return MapToReadDto(existingEmployee);
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id de l'employé invalide.", nameof(id));

            var existingEmployee = await _repository.GetByIdAsync(id);
            if (existingEmployee == null)
                throw new KeyNotFoundException($"Employé avec l'id {id} introuvable.");

            await _repository.DeleteEmployeeAsync(id);
        }
    }
}
