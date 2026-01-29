using EmployeeManagement.Models;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services
{
    // Service = couche métier (Business Logic)
    // C'est ici que l'on prend les décisions (existence, erreurs, règles)
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // Retourner tous les employés
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Retourner un employé par son Id
        public async Task<Employee?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _repository.GetByIdAsync(id);
        }

        // Ajouter un employé
        public async Task AddEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            await _repository.AddEmployeeAsync(employee);
        }

        // Mettre à jour un employé
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            if (employee.Id <= 0)
            {
                throw new ArgumentException("Id de l'employé invalide.");
            }

            // Vérifier si l'employé existe avant la mise à jour
            var existingEmployee = await _repository.GetByIdAsync(employee.Id);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException($"Employé avec l'id {employee.Id} introuvable.");
            }

            // Mise à jour des champs
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.Position = employee.Position;

            await _repository.UpdateEmployeeAsync(existingEmployee);
        }

        // Supprimer un employé
        public async Task DeleteEmployeeAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id de l'employé invalide.");
            }

            // Vérifier l'existence avant suppression
            var existingEmployee = await _repository.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException($"Employé avec l'id {id} introuvable.");
            }

            await _repository.DeleteEmployeeAsync(id);
        }
    }
}
