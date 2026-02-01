using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagement.Pages.Employees
{
    public class UpdateModel : PageModel
    {
        private readonly EmployeeService _service;

        public UpdateModel(EmployeeService service)
        {
            _service = service;
        }

        [BindProperty]
        public EmployeeDto Employee { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateAsync(Employee);
                return new JsonResult(
                    new { success = true, message = "Employee updated successfully!" }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
