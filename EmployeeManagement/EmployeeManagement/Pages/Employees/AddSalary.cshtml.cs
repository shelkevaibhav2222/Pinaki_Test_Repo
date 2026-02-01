using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagement.Pages.Employees
{
    public class AddSalaryModel : PageModel
    {
        private readonly EmployeeService _employeeService;
        private readonly EmployeeSalaryService _salaryService;

        public AddSalaryModel(EmployeeService employeeService, EmployeeSalaryService salaryService)
        {
            _employeeService = employeeService;
            _salaryService = salaryService;
        }

        [BindProperty]
        public EmployeeSalaryDto SalaryForm { get; set; } = new();

        public SelectList Employees { get; set; } = new(new List<EmployeeDto>());

        public async Task OnGetAsync()
        {
            var employees = await _employeeService.GetAllAsync();
            Employees = new SelectList(employees, "Id", "FirstName");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var employees = await _employeeService.GetAllAsync();
                Employees = new SelectList(employees, "Id", "FirstName");
                return Page();
            }

            try
            {
                await _salaryService.AddSalaryAsync(SalaryForm);
                return RedirectToPage("Index", new { message = "Salary added successfully!" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error saving salary: " + ex.Message);
                var employees = await _employeeService.GetAllAsync();
                Employees = new SelectList(employees, "Id", "FirstName");
                return Page();
            }
        }
    }
}
