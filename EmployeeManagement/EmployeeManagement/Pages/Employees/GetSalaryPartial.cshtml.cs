using System;
using System.Linq;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagement.Pages.Employees
{
    public class GetSalaryPartialModel : PageModel
    {
        private readonly EmployeeSalaryService _salaryService;

        public GetSalaryPartialModel(EmployeeSalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        public async Task<IActionResult> OnGetAsync(int id, int pageNumber = 1)
        {
            try
            {
                var salaries = await _salaryService.GetCurrentYearSalariesAsync(id);
                salaries ??= new List<EmployeeSalaryDto>();

                const int pageSize = 5;
                var totalCount = salaries.Count;
                var totalAmount = salaries.Sum(s => s.Amount);
                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                var currentPage = pageNumber < 1 ? 1 : pageNumber;
                if (totalPages > 0 && currentPage > totalPages)
                {
                    currentPage = totalPages;
                }

                var pagedSalaries = salaries
                    .OrderBy(s => s.Id)
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var model = new SalaryListViewModel
                {
                    EmployeeId = id,
                    Salaries = pagedSalaries,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalAmount = totalAmount,
                };

                return Partial("_Salary", model);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    $"<div class='alert alert-danger'>Error loading salary records: {ex.Message}</div>"
                );
            }
        }
    }
}
