using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/employee
        // Récupérer la liste de tous les employés
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeeAsync()
        {
            var allEmployees = await _employeeService.GetAllAsync();
            return Ok(allEmployees);
        }

        // GET: api/employee/{id}
        // Récupérer un employé par son Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST: api/employee
        // Créer un nouvel employé
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            await _employeeService.AddEmployeeAsync(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        // PUT: api/employee/{id}
        // Mettre à jour un employé existant
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployeAsync(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("L'id de l'URL ne correspond pas à l'id de l'employé.");
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _employeeService.UpdateEmployeeAsync(employee);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        // DELETE: api/employee/{id}
        // Supprimer un employé par son Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeById(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
