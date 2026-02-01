using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagement.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly EmployeeService _service;

        public CreateModel(EmployeeService service)
        {
            _service = service;
        }

        [BindProperty]
        public EmployeeDto EmployeeForm { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _service.AddAsync(EmployeeForm);
                return RedirectToPage("Index", new { message = "Employee added successfully!" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error saving employee: " + ex.Message);
                return Page();
            }
        }
    }
}
