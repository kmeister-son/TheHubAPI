using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheHubAPI.Data;
using TheHubAPI.Models;

namespace TheHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly TheHubDbContext _hubDbContext;

        public AdminController(TheHubDbContext hubDbContext)
        {
            _hubDbContext = hubDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _hubDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployee([FromBody] Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid();
            await _hubDbContext.Employees.AddAsync(employee);
            await _hubDbContext.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpGet]
        [Route("{employeeId:Guid}")]
        public async Task<IActionResult> EditEmployees([FromRoute] Guid employeeId)
        {
            var employee = await _hubDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{employeeId:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid employeeId, Employee updateEmployee)
        {
            var employee = await _hubDbContext.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployee.Name;
            employee.Email = updateEmployee.Email;
            employee.Phone = updateEmployee.Phone;

            await _hubDbContext.SaveChangesAsync();
            
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{employeeId:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid employeeId)
        {
            var employee = await _hubDbContext.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            _hubDbContext.Employees.Remove(employee);
            await _hubDbContext.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
