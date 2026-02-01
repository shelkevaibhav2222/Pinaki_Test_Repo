using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagement.Pages.Employees
{
    public partial class AddSalaryModel : PageModel
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

        [BindProperty]
        [Required(ErrorMessage = "Salary date is required.")]
        public string SalaryDateText { get; set; } = string.Empty;

        public SelectList Employees { get; set; } = new(new List<EmployeeDto>());

        public async Task OnGetAsync()
        {
            SalaryDateText = DateTime.Today.ToString(
                "dd/MM/yyyy",
                CultureInfo.GetCultureInfo("en-IN")
            );
            await LoadFormDataAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadFormDataAsync();
                return Page();
            }

            var formats = new[] { "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy" };
            if (
                !DateTime.TryParseExact(
                    SalaryDateText,
                    formats,
                    CultureInfo.GetCultureInfo("en-IN"),
                    DateTimeStyles.None,
                    out var selectedDate
                )
            )
            {
                ModelState.AddModelError(
                    nameof(SalaryDateText),
                    "Invalid date. Please use dd/MM/yyyy."
                );
                await LoadFormDataAsync();
                return Page();
            }

            try
            {
                SalaryForm.SalaryDate = selectedDate.Date;
                await _salaryService.AddSalaryAsync(SalaryForm);
                return RedirectToPage("Index", new { message = "Salary added successfully!" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error saving salary: " + ex.Message);
                await LoadFormDataAsync();
                return Page();
            }
        }

        private async Task LoadFormDataAsync()
        {
            var employees = await _employeeService.GetAllAsync();
            Employees = new SelectList(employees, "Id", "FirstName");
        }
    }
}
