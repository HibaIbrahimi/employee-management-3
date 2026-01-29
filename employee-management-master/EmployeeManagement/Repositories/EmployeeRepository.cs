using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    // Repository = couche d'accès aux données (Data Access Layer)
    // Il ne contient AUCUNE logique métier
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        // Récupérer tous les employés depuis la base de données
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        // Récupérer un employé par son Id
        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        // Ajouter un nouvel employé en base de données
        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync(); // Sauvegarde en base
        }

        // Mettre à jour un employé existant
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync(); // Sauvegarde en base
        }

        // Supprimer un employé par son Id
        // Le repository ne décide pas si l'employé existe ou non
        public async Task DeleteEmployeeAsync(Guid id)
        {
            var employeeInDb = await _context.Employees.FindAsync(id);

            // Si l'employé n'existe pas, on ne fait rien
            if (employeeInDb == null)
            {
                return;
            }

            _context.Employees.Remove(employeeInDb);
            await _context.SaveChangesAsync(); // Sauvegarde en base
        }
    }
}
