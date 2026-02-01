using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Pages.Employees
{
    public class EmployeesApiModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        private readonly EmployeeService _employeeService;
        private readonly EmployeeSalaryService _salaryService;

        public EmployeesApiModel(
            EmployeeService employeeService,
            EmployeeSalaryService salaryService
        )
        {
            _employeeService = employeeService;
            _salaryService = salaryService;
        }

        public async Task<IActionResult> OnGetGetEmployeePartialAsync(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Return partial view
            return Partial("_Employee", employee);
        }

        public async Task<IActionResult> OnGetGetSalaryPartialAsync(int id)
        {
            var salaries = await _salaryService.GetCurrentYearSalariesAsync(id);
            return Partial("_Salary", salaries);
        }
    }
}
