using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagement.Pages.Employees
{
    public class GetEmployeePartialModel : PageModel
    {
        private readonly EmployeeService _employeeService;

        public GetEmployeePartialModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound("Employee not found");
                }

                return Partial("_Employee", employee);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    $"<div class='alert alert-danger'>Error loading employee: {ex.Message}</div>"
                );
            }
        }
    }
}
