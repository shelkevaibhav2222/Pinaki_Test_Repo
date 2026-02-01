using System;
using System.Collections.Generic;
using EmployeeManagement.Application.DTOs;

namespace EmployeeManagement.Pages.Employees
{
    public class SalaryListViewModel
    {
        public List<EmployeeSalaryDto> Salaries { get; set; } = new();
        public int EmployeeId { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
