using EmployeeManagement.DTOs;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;

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
        // Retourne la liste des employés (DTO de lecture)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAllEmployeeAsync()
        {
            var allEmployees = await _employeeService.GetAllAsync();
            return Ok(allEmployees);
        }

        // GET: api/employee/{id}
        // Retourne un employé par Id
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // POST: api/employee
        // Crée un employé à partir d'un DTO de création
        [HttpPost]
        public async Task<ActionResult<EmployeeReadDto>> CreateEmployee(
            [FromBody] EmployeeCreateDto dto
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _employeeService.AddEmployeeAsync(dto);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = created.Id }, created);
        }

        // PUT: api/employee/{id}
        // Met à jour un employé à partir d'un DTO de mise à jour
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> UpdateEmployeAsync(
            int id,
            [FromBody] EmployeeUpdateDto dto
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _employeeService.UpdateEmployeeAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/employee/{id}
        // Supprime un employé
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeById(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
