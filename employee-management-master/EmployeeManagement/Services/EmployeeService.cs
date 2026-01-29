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
        private static EmployeeReadDto MapToReadDto(Employee e)
        {
            return new EmployeeReadDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Position = e.Position
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

        public async Task<EmployeeReadDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return null;

            return MapToReadDto(employee);
        }

        public async Task<EmployeeReadDto> AddEmployeeAsync(EmployeeCreateDto dto)
        {
            // Ici on peut ajouter des règles métier (ex: email unique) plus tard

            var employee = MapCreateDtoToEntity(dto);
            await _repository.AddEmployeeAsync(employee);

            // Après SaveChangesAsync, l'Id est rempli
            return MapToReadDto(employee);
        }

        public async Task<EmployeeReadDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("Id de l'employé invalide.", nameof(id));

            // Vérifier existence
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Employé avec l'id {id} introuvable.");

            // Appliquer mise à jour (on garde la même entity tracked)
            existing.FirstName = dto.FirstName.Trim();
            existing.LastName = dto.LastName.Trim();
            existing.Email = dto.Email.Trim();
            existing.Phone = dto.Phone.Trim();
            existing.Position = dto.Position.Trim();

            await _repository.UpdateEmployeeAsync(existing);

            return MapToReadDto(existing);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id de l'employé invalide.", nameof(id));

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Employé avec l'id {id} introuvable.");

            await _repository.DeleteEmployeeAsync(id);
        }
    }
}
